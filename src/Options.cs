using CommandLine;

namespace QuotefancyDownloader
{
    public class Options
    {
        public const string DownloadUrl = "https://quotefancy.com/download/{0}/original/wallpaper.jpg";

        [Option('s', "skip",
            Required = false,
            Default = 0,
            HelpText = "Starting image index for download")]
        public int Skip { get; set; }

        [Option('t', "take",
            Required = false,
            Default = 5,
            HelpText = "How many download tasks should be running at once")]
        public int Take { get; set; }

        [Option('e', "end",
            Required = false,
            Default = 500000,
            HelpText = "Last image index to download")]
        public int End { get; set; }

        [Option('o', "output",
            Required = false,
            Default = "Output",
            HelpText = "Output path where images will be saved")]
        public string OutputPath { get; set; }

        [Option('m', "mode",
            Required = false,
            Default = DownloadingMode.WaitForAll,
            HelpText = @"Downloading mode to use:
                - WaitForAll - Waits for all download tasks to end, then takes next tasks
                - Instant - Starts next download task immediately after finished previous task")]
        public DownloadingMode DownloadingMode { get; set; }
    }
}