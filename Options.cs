using CommandLine;
using CommandLine.Text;

namespace QuotefancyDownloader
{
    public class Options
    {
        public const string DownloadUrl = "https://quotefancy.com/download/{0}/original/wallpaper.jpg";

        [Option('s', "skip",
            Required = false,
            DefaultValue = 0,
            HelpText = "Starting image index for download")]
        public int Skip { get; set; }

        [Option('t', "take",
            Required = false,
            DefaultValue = 5,
            HelpText = "How many download tasks should be running at once")]
        public int Take { get; set; }

        [Option('e', "end",
            Required = false,
            DefaultValue = 500000,
            HelpText = "Last image index to download")]
        public int End { get; set; }

        [Option('o', "output",
            Required = false,
            DefaultValue = "Output",
            HelpText = "Output path where images will be saved")]
        public string OutputPath { get; set; }

        [Option('m', "mode",
            Required = false,
            DefaultValue = DownloadingMode.WaitForAll,
            HelpText = @"Downloading mode to use:
                - WaitForAll - Waits for all download tasks to end, then takes next tasks
                - Instant - Starts next download task immediately after finished previous task")]
        public DownloadingMode DownloadingMode { get; set; }

        [ParserState]
        public IParserState LastParserState { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
                current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}