using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace scopic_test_server.Services
{
    public class BidExpiryChecker : BackgroundService
    {
        private readonly IBidWorker _bidWorker;
        public BidExpiryChecker(IBidWorker bidWorker)
        {
            _bidWorker = bidWorker;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _bidWorker.CheckFinishedProducts(stoppingToken);
        }
    }

}