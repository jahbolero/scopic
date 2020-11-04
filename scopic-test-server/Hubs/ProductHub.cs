using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using scopic_test_server.DTO;

namespace scopic_test_server.Hubs
{
    public class ProductHub : Hub
    {
        public Task SendBidUpdate(ProductReadDto Product)
        {
            return Clients.All.SendAsync("ReceiveProduct", Product);
        }

    }
}