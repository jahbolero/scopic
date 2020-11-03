using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using scopic_test_server.Interface;

namespace scopic_test_server.Services
{
    public class BidWorker : IBidWorker
    {
        public readonly IServiceScopeFactory _scopeFactory;
        public BidWorker(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        public async Task CheckFinishedProducts(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();
            while (!cancellationToken.IsCancellationRequested)
            {
                productRepository.ProcessProducts();
                await Task.Delay(1000 * 10);
            }
        }
    }
}