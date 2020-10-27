using System;
using System.Collections.Generic;
using scopic_test_server.Data;
using scopic_test_server.DTO;
using static scopic_test_server.Helper.Codes;

namespace scopic_test_server.Interface
{
    public interface IBidRepository
    {
        IEnumerable<Bid> GetBidsByProduct(Guid ProductId);
        BidCode AddBid(BidCreateDto Bid);
    }
}