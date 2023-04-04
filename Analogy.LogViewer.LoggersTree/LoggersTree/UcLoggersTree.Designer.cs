
namespace Analogy.LogViewer.LoggersTree.LoggersTree
{
    partial class UcLoggersTree
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            TrvLoggers = new System.Windows.Forms.TreeView();
            cmsLogger = new System.Windows.Forms.ContextMenuStrip(components);
            setLogLevelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            allToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            verboseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            traceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            informationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            warningToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            errorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            criticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            offToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            Split = new System.Windows.Forms.SplitContainer();
            PnlToolbar = new System.Windows.Forms.Panel();
            BtnExpand = new System.Windows.Forms.Button();
            BtnCollapse = new System.Windows.Forms.Button();
            TxtQuery = new System.Windows.Forms.TextBox();
            cmsLogger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Split).BeginInit();
            Split.Panel1.SuspendLayout();
            Split.Panel2.SuspendLayout();
            Split.SuspendLayout();
            PnlToolbar.SuspendLayout();
            SuspendLayout();
            // 
            // TrvLoggers
            // 
            TrvLoggers.ContextMenuStrip = cmsLogger;
            TrvLoggers.Dock = System.Windows.Forms.DockStyle.Fill;
            TrvLoggers.Location = new System.Drawing.Point(0, 40);
            TrvLoggers.Name = "TrvLoggers";
            TrvLoggers.Size = new System.Drawing.Size(733, 469);
            TrvLoggers.TabIndex = 2;
            TrvLoggers.NodeMouseClick += TrvLoggers_NodeMouseClick;
            // 
            // cmsLogger
            // 
            cmsLogger.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { setLogLevelToolStripMenuItem });
            cmsLogger.Name = "contextMenuStrip1";
            cmsLogger.Size = new System.Drawing.Size(138, 26);
            // 
            // setLogLevelToolStripMenuItem
            // 
            setLogLevelToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { allToolStripMenuItem, verboseToolStripMenuItem, traceToolStripMenuItem, debugToolStripMenuItem, informationToolStripMenuItem, warningToolStripMenuItem, errorToolStripMenuItem, criticalToolStripMenuItem, offToolStripMenuItem });
            setLogLevelToolStripMenuItem.Name = "setLogLevelToolStripMenuItem";
            setLogLevelToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            setLogLevelToolStripMenuItem.Text = "Set log level";
            // 
            // allToolStripMenuItem
            // 
            allToolStripMenuItem.Image = Properties.Resources.all;
            allToolStripMenuItem.Name = "allToolStripMenuItem";
            allToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            allToolStripMenuItem.Text = "All";
            allToolStripMenuItem.Click += allToolStripMenuItem_Click;
            // 
            // verboseToolStripMenuItem
            // 
            verboseToolStripMenuItem.Image = Properties.Resources.verbose;
            verboseToolStripMenuItem.Name = "verboseToolStripMenuItem";
            verboseToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            verboseToolStripMenuItem.Text = "Verbose";
            verboseToolStripMenuItem.Click += verboseToolStripMenuItem_Click;
            // 
            // traceToolStripMenuItem
            // 
            traceToolStripMenuItem.Image = Properties.Resources.trace;
            traceToolStripMenuItem.Name = "traceToolStripMenuItem";
            traceToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            traceToolStripMenuItem.Text = "Trace";
            traceToolStripMenuItem.Click += traceToolStripMenuItem_Click;
            // 
            // debugToolStripMenuItem
            // 
            debugToolStripMenuItem.Image = Properties.Resources.debug;
            debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            debugToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            debugToolStripMenuItem.Text = "Debug";
            debugToolStripMenuItem.Click += debugToolStripMenuItem_Click;
            // 
            // informationToolStripMenuItem
            // 
            informationToolStripMenuItem.Image = Properties.Resources.information;
            informationToolStripMenuItem.Name = "informationToolStripMenuItem";
            informationToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            informationToolStripMenuItem.Text = "Information";
            informationToolStripMenuItem.Click += informationToolStripMenuItem_Click;
            // 
            // warningToolStripMenuItem
            // 
            warningToolStripMenuItem.Image = Properties.Resources.warning;
            warningToolStripMenuItem.Name = "warningToolStripMenuItem";
            warningToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            warningToolStripMenuItem.Text = "Warning";
            warningToolStripMenuItem.Click += warningToolStripMenuItem_Click;
            // 
            // errorToolStripMenuItem
            // 
            errorToolStripMenuItem.Image = Properties.Resources.error;
            errorToolStripMenuItem.Name = "errorToolStripMenuItem";
            errorToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            errorToolStripMenuItem.Text = "Error";
            errorToolStripMenuItem.Click += errorToolStripMenuItem_Click;
            // 
            // criticalToolStripMenuItem
            // 
            criticalToolStripMenuItem.Image = Properties.Resources.critical;
            criticalToolStripMenuItem.Name = "criticalToolStripMenuItem";
            criticalToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            criticalToolStripMenuItem.Text = "Critical";
            criticalToolStripMenuItem.Click += criticalToolStripMenuItem_Click;
            // 
            // offToolStripMenuItem
            // 
            offToolStripMenuItem.Image = Properties.Resources.off;
            offToolStripMenuItem.Name = "offToolStripMenuItem";
            offToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            offToolStripMenuItem.Text = "Off";
            offToolStripMenuItem.Click += offToolStripMenuItem_Click;
            // 
            // Split
            // 
            Split.Dock = System.Windows.Forms.DockStyle.Fill;
            Split.Location = new System.Drawing.Point(0, 0);
            Split.Name = "Split";
            Split.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // Split.Panel1
            // 
            Split.Panel1.Controls.Add(TrvLoggers);
            Split.Panel1.Controls.Add(PnlToolbar);
            // 
            // Split.Panel2
            // 
            Split.Panel2.Controls.Add(TxtQuery);
            Split.Size = new System.Drawing.Size(733, 615);
            Split.SplitterDistance = 509;
            Split.TabIndex = 3;
            // 
            // PnlToolbar
            // 
            PnlToolbar.Controls.Add(BtnExpand);
            PnlToolbar.Controls.Add(BtnCollapse);
            PnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            PnlToolbar.Location = new System.Drawing.Point(0, 0);
            PnlToolbar.Name = "PnlToolbar";
            PnlToolbar.Size = new System.Drawing.Size(733, 40);
            PnlToolbar.TabIndex = 3;
            // 
            // BtnExpand
            // 
            BtnExpand.Image = Properties.Resources.plus;
            BtnExpand.Location = new System.Drawing.Point(15, 7);
            BtnExpand.Name = "BtnExpand";
            BtnExpand.Size = new System.Drawing.Size(23, 23);
            BtnExpand.TabIndex = 1;
            BtnExpand.UseVisualStyleBackColor = true;
            BtnExpand.Click += BtnExpand_Click;
            // 
            // BtnCollapse
            // 
            BtnCollapse.Image = Properties.Resources.minus;
            BtnCollapse.Location = new System.Drawing.Point(44, 7);
            BtnCollapse.Name = "BtnCollapse";
            BtnCollapse.Size = new System.Drawing.Size(23, 23);
            BtnCollapse.TabIndex = 0;
            BtnCollapse.UseVisualStyleBackColor = true;
            BtnCollapse.Click += BtnCollapse_Click;
            // 
            // TxtQuery
            // 
            TxtQuery.AcceptsReturn = true;
            TxtQuery.AcceptsTab = true;
            TxtQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            TxtQuery.Location = new System.Drawing.Point(0, 0);
            TxtQuery.Multiline = true;
            TxtQuery.Name = "TxtQuery";
            TxtQuery.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            TxtQuery.Size = new System.Drawing.Size(733, 102);
            TxtQuery.TabIndex = 5;
            TxtQuery.WordWrap = false;
            // 
            // UcLoggersTree
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            AutoSize = true;
            Controls.Add(Split);
            Name = "UcLoggersTree";
            Size = new System.Drawing.Size(733, 615);
            cmsLogger.ResumeLayout(false);
            Split.Panel1.ResumeLayout(false);
            Split.Panel2.ResumeLayout(false);
            Split.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)Split).EndInit();
            Split.ResumeLayout(false);
            PnlToolbar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.TreeView TrvLoggers;
        private System.Windows.Forms.SplitContainer Split;
        private System.Windows.Forms.ContextMenuStrip cmsLogger;
        private System.Windows.Forms.ToolStripMenuItem setLogLevelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verboseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem traceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem informationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem warningToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem errorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem criticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offToolStripMenuItem;
        private System.Windows.Forms.TextBox TxtQuery;
        private System.Windows.Forms.Panel PnlToolbar;
        private System.Windows.Forms.Button BtnExpand;
        private System.Windows.Forms.Button BtnCollapse;
    }
}
