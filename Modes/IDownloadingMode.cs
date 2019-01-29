using System.Threading.Tasks;

namespace QuotefancyDownloader.Modes
{
    public interface IDownloadingMode
    {
        Task StartAsync();
    }
}