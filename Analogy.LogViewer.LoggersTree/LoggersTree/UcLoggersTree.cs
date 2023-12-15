using Analogy.Interfaces;
using Analogy.LogViewer.LoggersTree.Properties;
using Analogy.LogViewer.LoggersTree.Utils;
using DevExpress.Skins;
using DevExpress.XtraBars.Docking;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Analogy.LogViewer.LoggersTree.LoggersTree
{
    /// <summary>
    /// The UI.
    /// </summary>
    public partial class UcLoggersTree : UserControl
    {
        private ILogRawSQL? parent;
        private string? currentQuery;
        private string? overloadedQuery;
        private ConcurrentQueue<IAnalogyLogMessage> MsgQueue { get; set; }
        private readonly System.Timers.Timer timer;
        private DockPanel? dockPanel;
        private ControlContainer? container;
        private bool isInitialized;

        private enum LogLevel
        {
            Critical,
            Error,
            Warning,
            Information,
            Debug,
            Verbose,
            Trace,
            All,
            Off,
        }

        public UcLoggersTree()
        {
            InitializeComponent();

            ImageList imageList = new ImageList();
            imageList.Images.Add(Resources.critical);
            imageList.Images.Add(Resources.error);
            imageList.Images.Add(Resources.warning);
            imageList.Images.Add(Resources.information);
            imageList.Images.Add(Resources.debug);
            imageList.Images.Add(Resources.verbose);
            imageList.Images.Add(Resources.trace);
            imageList.Images.Add(Resources.all);
            imageList.Images.Add(Resources.off);
            TrvLoggers.ImageList = imageList;
            TrvLoggers.ImageIndex = (int)LogLevel.All;
            TrvLoggers.SelectedImageIndex = (int)LogLevel.All;

            MsgQueue = new ConcurrentQueue<IAnalogyLogMessage>();

            Skin currentSkin = CommonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default);
            if (currentSkin != null)
            {
                Color bg = currentSkin.TranslateColor(SystemColors.Control);
                Color fg = currentSkin.TranslateColor(SystemColors.ControlText);
                TrvLoggers.BackColor = bg;
                TrvLoggers.ForeColor = fg;
                TxtQuery.BackColor = bg;
                TxtQuery.ForeColor = fg;
            }
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += ReadQueue;
            timer.Start();
        }

        private void ReadAll()
        {
            if (IsDisposed)
            {
                return;
            }
            int sleep = 0;
            for (int i = 0; i < MsgQueue.Count; i++)
            {
                if (!MsgQueue.TryDequeue(out IAnalogyLogMessage? message))
                {
                    timer.Start();
                    return;
                }
                TrvLoggers.BeginInvoke((MethodInvoker)(() => { AppendMessage(message); }));
                sleep++;
                if (sleep >= 500)
                {
                    Thread.Sleep(100);
                    sleep = 0;
                }
            }
            if (!isInitialized)
            {
#pragma warning disable MA0134
                Task.Factory.StartNew(() =>
                {
                    //wait for UI to be ready
                    Thread.Sleep(5000);
                    TrvLoggers.BeginInvoke((MethodInvoker)(() => { TrvLoggers.ExpandAll(); }));
                }, TaskCreationOptions.LongRunning);
#pragma warning restore MA0134
            }
            isInitialized = true;
        }

        private void ReadQueue(object? sender, ElapsedEventArgs elapsedEventArgs)
        {
            timer.Stop();
            ReadAll();
            timer.Start();
        }

        private void AppendMessage(IAnalogyLogMessage message)
        {
            Logger log = new Logger { Machine = message.MachineName, ProcessName = message.Module, Source = message.Source };
            if (!TrvLoggers.Nodes.ContainsKey(log.ProcessKey))
            {
                TrvLoggers.Nodes.Add(log.ProcessKey, log.ProcessKey, (int)LogLevel.All);
            }
            TreeNode? node = TrvLoggers.Nodes.Find(log.ProcessKey, false).FirstOrDefault();
            if (node == null)
            {
                return;
            }

            string[] generics = log.Source?.Split('`') ?? Array.Empty<string>();
            if (generics.Any())
            {
                string[] path = generics[0].Split('.');
                for (int i = 0; i < path.Length - 1; i++)
                {
                    string current = string.Empty;
                    for (int j = 0; j <= i; j++)
                    {
                        if (current != string.Empty)
                        {
                            current += ".";
                        }

                        current += path[j];
                    }

                    if (!node.Nodes.ContainsKey(current))
                    {
                        node = node.Nodes.Add(current, current, (int)LogLevel.All);
                    }
                    else
                    {
                        node = node.Nodes.Find(current, false).Single();
                    }
                }
            }
            if (!node.Nodes.ContainsKey(log.Source))
            {
                node.Nodes.Add(log.Source, log.Source, (int)LogLevel.All);
            }
        }

        public void Clear()
        {
            TrvLoggers.Nodes.Clear();
#if NET5_0_OR_GREATER
            MsgQueue.Clear();
#else
            while (!MsgQueue.IsEmpty)
            {
                MsgQueue.TryDequeue(out _);
            }
#endif
        }

        public void NewMessage(IAnalogyLogMessage message)
        {
            MsgQueue.Enqueue(message);
        }

        public void SetLogRawSQL(ILogRawSQL logRawSql)
        {
            parent = logRawSql;
            parent.OnRawSQLFilterChanged += Parent_OnSetRawSQLFilter;
        }

        private void Parent_OnSetRawSQLFilter(object? sender, string query)
        {
            if (!string.Equals(query, overloadedQuery, StringComparison.OrdinalIgnoreCase))
            {
                currentQuery = query;
            }
        }

        private string CreateLevelQuery(string requestedLevel)
        {
            string levelQuery = string.Empty;
            List<string> levels = new List<string>()
                                  {
                                      "Critical",
                                      "Error",
                                      "Warning",
                                      "Information",
                                      "Debug",
                                      "Verbose",
                                      "Trace",
                                  };
            foreach (string l in levels)
            {
                if (QueryLevel(ref levelQuery, l, requestedLevel))
                {
                    break;
                }
            }
            return levelQuery;
        }

        private bool QueryLevel(ref string levelQuery, string level, string requestedLevel)
        {
            if (levelQuery.Length > 0)
            {
                levelQuery += " OR ";
            }

            levelQuery += $"Level = '{level}'";
            if (string.Equals(requestedLevel, level, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            return false;
        }

        private void SetLogLevel(LogLevel level)
        {
            TreeNode node = TrvLoggers.SelectedNode;
            SetLogLevel(node, level);

            //set default query if we didn't get it from event
            if (string.IsNullOrEmpty(currentQuery))
            {
                currentQuery = "( Text like '%%') AND (Date >= '01/01/0001 00:00:00' and Date <= '31/12/9999 23:59:59')";
            }

            overloadedQuery = currentQuery;
            foreach (TreeNode root in TrvLoggers.Nodes)
            {
                if (root != null)
                {
                    CreateQuery(root, LogLevel.All);
                }
            }
            TxtQuery.Text = SqlPrettify.Pretty(overloadedQuery);
            if (overloadedQuery != null)
            {
                parent?.ApplyRawSQLFilter(overloadedQuery);
            }
        }

        private void CreateQuery(TreeNode node, LogLevel parentLevel)
        {
            LogLevel level = (LogLevel)node.ImageIndex;
            if (node.ImageIndex != (int)parentLevel)
            {
                TreeNode root = GetRootNode(node);
                string[] data = root.Text.Split(Logger.Separator);
                if (data.Length != 2)
                {
                    throw new NotSupportedException($"ProcessKey is malformed: {root.Text}");
                }

                string processQuery = $"([MachineName] = '{data.First()}' AND Module = '{data.Last()}')";
                string nodeTextEscaped = node.Text.Replace("[", "[[]").Replace("=", "[=]");
                if (level == LogLevel.Off)
                {
                    if (root == node)
                    {
                        overloadedQuery += $" AND NOT {processQuery}";
                    }
                    else
                    {
                        overloadedQuery += $" AND ((Source not like '{nodeTextEscaped}%') OR not {processQuery})";
                    }
                }
                else
                {
                    string levelQuery = CreateLevelQuery(level.ToString());
                    if (root == node)
                    {
                        overloadedQuery += $" AND ((({levelQuery}) AND {processQuery}) OR not {processQuery})";
                    }
                    else
                    {
                        if (parentLevel != LogLevel.Off && level <= parentLevel)
                        {
                            overloadedQuery += $" AND ((Source like '{nodeTextEscaped}%' AND ({levelQuery}) AND {processQuery}) OR Source not like '{nodeTextEscaped}%' OR not {processQuery})";
                        }
                        else
                        {
                            overloadedQuery += $" OR (Source like '{nodeTextEscaped}%' AND ({levelQuery}) AND {processQuery})";
                        }
                    }
                }
            }
            foreach (TreeNode child in node.Nodes)
            {
                if (child != null)
                {
                    CreateQuery(child, level);
                }
            }
        }

        private TreeNode GetRootNode(TreeNode node)
        {
            if (node.Parent == null)
            {
                return node;
            }

            return GetRootNode(node.Parent);
        }

        private void SetLogLevel(TreeNode node, LogLevel level)
        {
            node.Tag = level;
            node.ImageIndex = (int)level;
            node.SelectedImageIndex = (int)level;
            foreach (TreeNode child in node.Nodes)
            {
                if (child != null)
                {
                    SetLogLevel(child, level);
                }
            }
        }

        private void AllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLogLevel(LogLevel.All);
        }

        private void VerboseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLogLevel(LogLevel.Verbose);
        }

        private void TraceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLogLevel(LogLevel.Trace);
        }

        private void DebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLogLevel(LogLevel.Debug);
        }

        private void InformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLogLevel(LogLevel.Information);
        }

        private void WarningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLogLevel(LogLevel.Warning);
        }

        private void ErrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLogLevel(LogLevel.Error);
        }

        private void CriticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLogLevel(LogLevel.Critical);
        }

        private void OffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLogLevel(LogLevel.Off);
        }

        private void TrvLoggers_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TrvLoggers.SelectedNode = e.Node;
            }
        }

        public void Init()
        {
            if (dockPanel != null)
            {
                dockPanel.Resize -= DockPanel_Resize;
            }
            Clear();
            if (Parent is ControlContainer cc)
            {
                container = cc;
                Size = container.Size;
                dockPanel = cc.Panel;
                dockPanel.Resize += DockPanel_Resize;
            }
        }

        private void DockPanel_Resize(object? sender, EventArgs e)
        {
            if (dockPanel != null && container != null)
            {
                Size = new Size(dockPanel.Size.Width - container.Margin.Left - container.Margin.Right, dockPanel.Size.Height - container.Margin.Top - container.Margin.Bottom);
            }
        }

        private void BtnExpand_Click(object sender, EventArgs e)
        {
            TrvLoggers.ExpandAll();
        }

        private void BtnCollapse_Click(object sender, EventArgs e)
        {
            TrvLoggers.CollapseAll();
        }
    }
}