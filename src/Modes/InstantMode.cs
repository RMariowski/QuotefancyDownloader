using System.Threading.Tasks;

namespace QuotefancyDownloader.Modes
{
    public class InstantMode : IDownloadingMode
    {
        private readonly Options _options;

        public InstantMode(Options options)
        {
            _options = options;
        }

        public Task StartAsync()
        {
            return Task.CompletedTask;
        }
    }
}