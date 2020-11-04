using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using scopic_test_server.DTO;

namespace scopic_test_server.Hubs
{
    public class BidHub : Hub
    {
        public Task SendBidUpdate(BidReadDto Bid)
        {
            return Clients.All.SendAsync("ReceiveBid", Bid);
        }

    }
}