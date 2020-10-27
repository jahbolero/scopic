using System;

namespace scopic_test_server.DTO
{
    public class BidCreateDto
    {
        public Guid BidId { get; set; }
        public decimal BidAmount { get; set; }
        public DateTime BidDate { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}