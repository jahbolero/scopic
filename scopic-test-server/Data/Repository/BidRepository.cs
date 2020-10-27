using System;
using System.Collections.Generic;
using AutoMapper;
using scopic_test_server.DTO;
using scopic_test_server.Helper;
using scopic_test_server.Interface;
using System.Linq;
using static scopic_test_server.Helper.Codes;
using Microsoft.EntityFrameworkCore;

namespace scopic_test_server.Data
{
    public class BidRepository : IBidRepository
    {
        private readonly ScopicContext _context;
        private readonly IMapper _mapper;

        public BidRepository(ScopicContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public BidCode AddBid(BidCreateDto Bid)
        {
            var bid = _mapper.Map<Bid>(Bid);
            bid.BidDate = DateTime.UtcNow;
            bid.BidId = Guid.NewGuid();
            var latestBid = _context.Bid.FirstOrDefault(x => x.ProductId == bid.ProductId);
            if (latestBid != null)
            {
                var product = _context.Product.FirstOrDefault(x => x.ProductId == bid.ProductId);
                if (product == null)
                    return BidCode.Null;
                if (bid.BidAmount <= latestBid.BidAmount)
                    return BidCode.PriceTooLow;
                if (bid.UserId == latestBid.UserId)
                    return BidCode.HighestBid;
            }
            _context.Bid.Add(bid);
            _context.SaveChanges();
            return Codes.BidCode.Success;
        }

        public IEnumerable<Bid> GetBidsByProduct(Guid ProductId)
        {
            var bidList = _context.Bid.Where(x => x.ProductId == ProductId).Include(y => y.User);
            return bidList;
        }
    }
}