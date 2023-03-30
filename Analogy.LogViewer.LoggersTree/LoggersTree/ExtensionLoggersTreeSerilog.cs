using System;

namespace Analogy.LogViewer.LoggersTree.LoggersTree
{
    public class ExtensionLoggersTreeSerilog : ExtensionLoggersTree
    {
        public override Guid Id { get; set; } = new Guid("7ACAA8F7-3513-4A9E-92A0-AFE7E5415C57");
        public override Guid TargetComponentId { get; set; } = new Guid("d89318c6-306a-48d9-90a0-7c2c49efda82");

        public override string Title { get; set; } = "Loggers tree for Serilog";
        public override string Description { get; set; } = "Loggers tree for Serilog";
    }
}