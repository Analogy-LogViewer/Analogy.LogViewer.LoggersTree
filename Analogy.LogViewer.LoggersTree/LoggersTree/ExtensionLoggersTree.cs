using Analogy.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Analogy.LogViewer.LoggersTree.LoggersTree
{
    public abstract class ExtensionLoggersTree : IAnalogyExtensionUserControl
    {
        public abstract Guid Id { get; set; }
        public abstract string Title { get; set; }
        public abstract Guid TargetComponentId { get; set; }
        public string Author { get; set; } = "CAMAG";
        public string AuthorMail { get; set; } = "info@camag.com";
        public List<string> AdditionalContributors { get; } = new List<string>(0);
        public abstract string Description { get; set; }
        private readonly Dictionary<Guid, UserControl> userControls;

        protected ExtensionLoggersTree()
        {
            userControls = new Dictionary<Guid, UserControl>();
        }

        public UserControl CreateUserControl(Guid logWindowsId, ILogger logger)
        {
            UcLoggersTree control = new UcLoggersTree();
            userControls.Add(logWindowsId, control);
            return control;
        }

        public UserControl GetUserControl(Guid logWindowsId)
        {
            return userControls[logWindowsId];
        }

        Task IAnalogyExtensionUserControl.InitializeUserControl(Control hostingControl, Guid logWindowsId, ILogger logger)
        {
            (GetUserControl(logWindowsId) as UcLoggersTree)?.Init();
            if (hostingControl is ILogRawSQL logRawSQL)
            {
                (GetUserControl(logWindowsId) as UcLoggersTree)?.SetLogRawSQL(logRawSQL);
            }

            return Task.CompletedTask;
        }

        public virtual void NewMessage(IAnalogyLogMessage message, Guid logWindowsId)
        {
            (GetUserControl(logWindowsId) as UcLoggersTree)?.NewMessage(message);
        }

        public void NewMessages(List<IAnalogyLogMessage> messages, Guid logWindowsId)
        {
            foreach (IAnalogyLogMessage message in messages)
            {
                NewMessage(message, logWindowsId);
            }
        }
    }
}