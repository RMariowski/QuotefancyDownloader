using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuotefancyDownloader.Modes
{
    public class WaitForAllMode : IDownloadingMode
    {
        private readonly Options _options;

        public WaitForAllMode(Options options)
        {
            _options = options;
        }

        public async Task StartAsync()
        {
            var tasks = new List<Task>();

            for (int start = _options.Skip; start <= _options.End; start += _options.Take)
            {
                int end = start + _options.Take;

                Console.WriteLine($"Starting from {start} to {end}");
                for (int index = start; index <= end; index++)
                {
                    var task = Program.DownloadImageAsync(index, _options);
                    tasks.Add(task);
                }

                await Task.WhenAll(tasks);

                tasks.Clear();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}
