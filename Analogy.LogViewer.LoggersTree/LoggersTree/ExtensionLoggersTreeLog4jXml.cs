using System;

namespace Analogy.LogViewer.LoggersTree.LoggersTree
{
    // ReSharper disable once InconsistentNaming
    public class ExtensionLoggersTreeLog4jXml : ExtensionLoggersTree
    {
        public override Guid Id { get; set; } = new Guid("71E8A074-0F81-4A9D-803A-F7043F3319B5");
        public override Guid TargetComponentId { get; set; } = new Guid("f17bf58c-01b7-49b7-9515-cf642fc021ac");
        public override string Title { get; set; } = "Loggers tree for Log4jXml";
        public override string Description { get; set; } = "Loggers tree for Log4jXml";
    }
}