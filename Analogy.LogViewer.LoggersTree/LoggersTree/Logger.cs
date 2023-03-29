namespace Analogy.LogViewer.LoggersTree.LoggersTree
{
    internal class Logger
    {
        public static char Separator = ':';

        public string Source { get; set; }
        public string Machine { get; set; }
        public string ProcessName { get; set; }

        public string ProcessKey
        {
            get { return Machine?.ToLower() + Separator + ProcessName?.ToLower(); }
        }

        public string SourceKey
        {
            get { return ProcessKey + Separator + Source; }
        }
    }
}