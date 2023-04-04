using System;
using System.Collections.Generic;
using System.Drawing;
using Analogy.Interfaces;
using Analogy.LogViewer.LoggersTree.Properties;

namespace Analogy.LogViewer.LoggersTree.LoggersTree
{
    public class PrimaryFactory : global::Analogy.LogViewer.Template.PrimaryFactory
    {
        internal static readonly Guid Id = new Guid("D33BA8D1-C22E-48B0-BCA5-4EFA125A52B4");
        public override Guid FactoryId { get; set; } = Id;

        public override string Title { get; set; } = "Loggers tree";
        public override Image? SmallImage { get; set; } = Resources.Analogy_image_16x16;
        public override Image? LargeImage { get; set; } = Resources.Analogy_image_32x32;

        public override IEnumerable<IAnalogyChangeLog> ChangeLog { get; set; } = new List<AnalogyChangeLog>
        {
            new AnalogyChangeLog("Corrected loggers' name in case of C# generics, corrected SQL generation, prettified displayed SQL",AnalogChangeLogType.Bug, "CAMAG",new DateTime(2023, 04, 04)),
            new AnalogyChangeLog("Corrected query and added auto resize",AnalogChangeLogType.Bug, "CAMAG",new DateTime(2023, 03, 22))
        };
        public override IEnumerable<string> Contributors { get; set; } = new List<string> { "CAMAG" };
        public override string About { get; set; } = "Loggers tree";
    }
}
