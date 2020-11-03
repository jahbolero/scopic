using System;
using System.Collections.Generic;
using AutoMapper;
using scopic_test_server.DTO;
using scopic_test_server.Helper;
using scopic_test_server.Interface;
using System.Linq;
using static scopic_test_server.Helper.Codes;
using Microsoft.EntityFrameworkCore;
using scopic_test_server.Services;

namespace scopic_test_server.Data
{
    public class BidRepository : IBidRepository
    {
        private readonly ScopicContext _context;
        private readonly IMapper _mapper;
        private readonly EmailService _emailService;

        public BidRepository(ScopicContext context, IMapper mapper, EmailService emailService)
        {
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
        }
        public BidCode AddBid(BidCreateDto Bid)
        {
            var bid = _mapper.Map<Bid>(Bid);
            bid.BidDate = DateTime.UtcNow;
            bid.BidId = Guid.NewGuid();
            var latestBid = _context.Bid.OrderByDescending(x => x.BidAmount).FirstOrDefault(x => x.ProductId == bid.ProductId);
            if (latestBid != null)
            {
                var product = _context.Product.FirstOrDefault(x => x.ProductId == bid.ProductId);
                if (product == null)
                    return BidCode.Null;
                if (bid.BidAmount <= latestBid.BidAmount)
                    return BidCode.PriceTooLow;
                if (bid.UserId == latestBid.UserId)
                    return BidCode.HighestBid;
                var bidders = product.Bids.Select(x => x.User).Distinct();
                foreach (var bidder in bidders)
                {
                    if (bidder.UserId != Bid.UserId)
                    {
                        var message = $"<h3>A new bid has been made for {product.ProductName}!</h3><p>Current highest bid amount:{bid.BidAmount}</p>";
                        var mail = _emailService.NewMail(bidder.Username, $"Product Update {product.ProductName}", message);
                    }

                }
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