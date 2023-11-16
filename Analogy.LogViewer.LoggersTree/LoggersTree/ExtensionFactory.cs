using Analogy.Interfaces;
using Analogy.Interfaces.Factories;
using System;
using System.Collections.Generic;

namespace Analogy.LogViewer.LoggersTree.LoggersTree
{
    // ReSharper disable once UnusedMember.Global
    public class ExtensionFactory : IAnalogyExtensionsFactory
    {
        public Guid FactoryId { get; set; } = PrimaryFactory.Id;
        public string Title { get; set; } = "Loggers tree extension";
        public IEnumerable<IAnalogyExtension> Extensions { get; } = new List<IAnalogyExtension> { new ExtensionLoggersTreeSerilog(), new ExtensionLoggersTreeLog4jXml() };
    }
}