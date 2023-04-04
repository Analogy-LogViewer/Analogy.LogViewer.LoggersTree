using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Analogy.Interfaces;
using Analogy.LogViewer.LoggersTree.Properties;
using Analogy.LogViewer.LoggersTree.Utils;
using DevExpress.Skins;
using DevExpress.XtraBars.Docking;

namespace Analogy.LogViewer.LoggersTree.LoggersTree
{
    public partial class UcLoggersTree : UserControl
    {

        private ILogRawSQL? _parent;
        private string? _currentQuery;
        private string? _overloadedQuery;
        private readonly ConcurrentQueue<IAnalogyLogMessage> _msgQueue;
        private readonly System.Timers.Timer _timer;
        private DockPanel? _dockPanel;
        private ControlContainer? _container;

        enum LogLevel
        {
            Critical,
            Error,
            Warning,
            Information,
            Debug,
            Verbose,
            Trace,
            All,
            Off
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

            _msgQueue = new ConcurrentQueue<IAnalogyLogMessage>();


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
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += ReadQueue;
            _timer.Start();
        }


        private void ReadQueue(object? sender, ElapsedEventArgs elapsedEventArgs)
        {
            _timer.Stop();
            for (int i = 0; i < 500; i++)
            {
                if (!_msgQueue.TryDequeue(out IAnalogyLogMessage? message))
                {
                    _timer.Start();
                    return;
                }
                TrvLoggers.BeginInvoke((MethodInvoker)delegate { AppendMessage(message); });
            }
            TrvLoggers.BeginInvoke((MethodInvoker)delegate { TrvLoggers.ExpandAll(); });
            Thread.Sleep(100);
            _timer.Start();
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
                return;
            string[] generics = log.Source.Split('`');
            string[] path = generics[0].Split('.');
            for (int i = 0; i < path.Length - 1; i++)
            {
                string current = string.Empty;
                for (int j = 0; j <= i; j++)
                {
                    if (current != String.Empty)
                        current += ".";
                    current += path[j];
                }
                if (!node.Nodes.ContainsKey(current))
                    node = node.Nodes.Add(current, current, (int)LogLevel.All);
                else
                    node = node.Nodes.Find(current, false).Single();
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
            _msgQueue.Clear();
#else
            while (!_msgQueue.IsEmpty)
                _msgQueue.TryDequeue(out _);
#endif
        }

        public void NewMessage(IAnalogyLogMessage message)
        {
            _msgQueue.Enqueue(message);

        }

        public void SetLogRawSQL(ILogRawSQL logRawSql)
        {
            _parent = logRawSql;
            _parent.OnRawSQLFilterChanged += Parent_OnSetRawSQLFilter;
        }

        private void Parent_OnSetRawSQLFilter(object? sender, string query)
        {
            if (query != _overloadedQuery)
                _currentQuery = query;
        }



        private string CreateLevelQuery(string requestedLevel)
        {
            string levelQuery = String.Empty;
            List<string> levels = new List<string>()
                                  {
                                      "Critical",
                                      "Error",
                                      "Warning",
                                      "Information",
                                      "Debug",
                                      "Verbose",
                                      "Trace"
                                  };
            foreach (string l in levels)
            {
                if (QueryLevel(ref levelQuery, l, requestedLevel))
                    break;
            }
            return levelQuery;
        }

        private bool QueryLevel(ref string levelQuery, string level, string requestedLevel)
        {
            if (levelQuery.Length > 0)
                levelQuery += " OR ";
            levelQuery += $"Level = '{level}'";
            if (requestedLevel == level)
                return true;
            return false;
        }


        private void SetLogLevel(LogLevel level)
        {
            TreeNode node = TrvLoggers.SelectedNode;
            SetLogLevel(node, level);
            //set default query if we didn't get it from event
            if (String.IsNullOrEmpty(_currentQuery))
                _currentQuery = "( Text like '%%') AND (Date >= '01/01/0001 00:00:00' and Date <= '31/12/9999 23:59:59')";
            _overloadedQuery = _currentQuery;
            foreach (TreeNode root in TrvLoggers.Nodes)
            {
                if (root != null)
                    CreateQuery(root, LogLevel.All);
            }
            TxtQuery.Text = SqlPrettify.Pretty(_overloadedQuery);
            if (_overloadedQuery != null)
                _parent?.ApplyRawSQLFilter(_overloadedQuery);
        }

        private void CreateQuery(TreeNode node, LogLevel parentLevel)
        {
            LogLevel level = (LogLevel)node.ImageIndex;
            if (node.ImageIndex != (int)parentLevel)
            {
                TreeNode root = GetRootNode(node);
                string[] data = root.Text.Split(Logger.Separator);
                if (data.Length != 2)
                    throw new NotSupportedException($"ProcessKey is malformed: {root.Text}");
                string processQuery = $"([MachineName] = '{data.First()}' AND Module = '{data.Last()}')";
                string nodeTextEscaped = node.Text.Replace("[", "[[]").Replace("=", "[=]");
                if (level == LogLevel.Off)
                {
                    if (root == node)
                        _overloadedQuery += $" AND NOT {processQuery}";
                    else
                        _overloadedQuery += $" AND ((Source not like '{nodeTextEscaped}%') OR not {processQuery})";
                }
                else
                {
                    string levelQuery = CreateLevelQuery(level.ToString());
                    if (root == node)
                        _overloadedQuery += $" AND ((({levelQuery}) AND {processQuery}) OR not {processQuery})";
                    else
                    {
                        if (parentLevel != LogLevel.Off && level <= parentLevel)
                            _overloadedQuery += $" AND ((Source like '{nodeTextEscaped}%' AND ({levelQuery}) AND {processQuery}) OR Source not like '{nodeTextEscaped}%' OR not {processQuery})";
                        else
                            _overloadedQuery += $" OR (Source like '{nodeTextEscaped}%' AND ({levelQuery}) AND {processQuery})";
                    }
                }
            }
            foreach (TreeNode child in node.Nodes)
                if (child != null)
                    CreateQuery(child, level);
        }

        private TreeNode GetRootNode(TreeNode node)
        {
            if (node.Parent == null)
                return node;
            return GetRootNode(node.Parent);
        }

        private void SetLogLevel(TreeNode node, LogLevel level)
        {
            node.Tag = level;
            node.ImageIndex = (int)level;
            node.SelectedImageIndex = (int)level;
            foreach (TreeNode child in node.Nodes)
                if (child != null)
                    SetLogLevel(child, level);
        }

        private void allToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLogLevel(LogLevel.All);
        }

        private void verboseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLogLevel(LogLevel.Verbose);
        }

        private void traceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLogLevel(LogLevel.Trace);
        }

        private void debugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLogLevel(LogLevel.Debug);
        }

        private void informationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLogLevel(LogLevel.Information);
        }

        private void warningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLogLevel(LogLevel.Warning);
        }

        private void errorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLogLevel(LogLevel.Error);
        }

        private void criticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLogLevel(LogLevel.Critical);
        }

        private void offToolStripMenuItem_Click(object sender, EventArgs e)
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
            if (_dockPanel != null)
            {
                _dockPanel.Resize -= _dockPanel_Resize;
            }
            Clear();
            if (Parent is ControlContainer cc)
            {
                _container = cc;
                Size = _container.Size;
                _dockPanel = cc.Panel;
                _dockPanel.Resize += _dockPanel_Resize;
            }
        }

        private void _dockPanel_Resize(object? sender, EventArgs e)
        {
            if (_dockPanel != null && _container != null)
                Size = new Size(_dockPanel.Size.Width - _container.Margin.Left - _container.Margin.Right, _dockPanel.Size.Height - _container.Margin.Top - _container.Margin.Bottom);
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
