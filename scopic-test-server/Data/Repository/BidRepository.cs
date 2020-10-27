using System;
using System.Collections.Generic;
using scopic_test_server.DTO;
using scopic_test_server.Interface;

namespace scopic_test_server.Data
{
    public class BidRepository : IBidRepository
    {
        public Bid AddBid(BidCreateDto Bid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bid> GetAllBids(Guid ProductId)
        {
            throw new NotImplementedException();
        }
    }
}