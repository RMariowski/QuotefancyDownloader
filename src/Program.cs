using QuotefancyDownloader.Modes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using CommandLine;

namespace QuotefancyDownloader
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptionsAndReturnExitCode)
                .WithNotParsed(HandleParseError);

            return 0;
        }

        private static void RunOptionsAndReturnExitCode(Options options)
        {
            CheckForOutputDirectory(options.OutputPath);

            Console.WriteLine("Start downloading");

            StartDownloading(options).GetAwaiter().GetResult();

            Console.WriteLine("Download finished");
        }

        private static void CheckForOutputDirectory(string outputDirectory)
        {
            outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), outputDirectory);
            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);
        }

        private static Task StartDownloading(Options options)
        {
            IDownloadingMode downloadingMode = null;

            switch (options.DownloadingMode)
            {
                case DownloadingMode.WaitForAll:
                    downloadingMode = new WaitForAllMode(options);
                    break;

                case DownloadingMode.Instant:
                    downloadingMode = new InstantMode(options);
                    break;
            }

            return downloadingMode?.StartAsync();
        }

        public static async Task DownloadImageAsync(int index, Options options)
        {
            var uri = new Uri(string.Format(Options.DownloadUrl, index));
            var request = WebRequest.Create(uri);
            var response = await request.GetResponseAsync();

            bool isImage = response.ContentType.Contains("image");
            if (!isImage)
            {
                Console.WriteLine($"{index} is not image");
                return;
            }

            SaveAsFile(index, response, options);
        }

        private static void SaveAsFile(int index, WebResponse response, Options options)
        {
            string path = Path.Combine(options.OutputPath, $"quotefancy{index}.jpg");

            using (var stream = response.GetResponseStream())
            {
                if (stream is null)
                    return;

                using (var fileStream = File.Create(path))
                    stream.CopyTo(fileStream);
            }
        }

        private static void HandleParseError(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
                Console.WriteLine(error);
        }
    }
}
