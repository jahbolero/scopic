using System;
using System.Collections.Generic;
using scopic_test_server.Data;
using scopic_test_server.DTO;

namespace scopic_test_server.Interface
{
    public interface IBidRepository
    {
        IEnumerable<Bid> GetAllBids(Guid ProductId);
        Bid AddBid(BidCreateDto Bid);
    }
}