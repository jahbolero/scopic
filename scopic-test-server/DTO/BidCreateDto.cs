using System;

namespace scopic_test_server.DTO
{
    public class BidCreateDto
    {
        public decimal BidAmount { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
    }
}