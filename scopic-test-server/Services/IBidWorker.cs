using System.Threading;
using System.Threading.Tasks;

namespace scopic_test_server.Services
{
    public interface IBidWorker
    {
        Task CheckFinishedProducts(CancellationToken cancellationToken);

    }
}