using System;
using scopic_test_server.Data;

namespace scopic_test_server.DTO
{
    public class BidReadDto
    {
        public Guid BidId { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime BidDate { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}