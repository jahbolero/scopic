using System;
using System.Collections.Generic;

namespace scopic_test_server.DTO
{
    public class ProductReadDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ImgUrl { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime UploadDate { get; set; }
        public IEnumerable<BidReadDto> Bids { get; set; }
        public UserProductDto UserProduct { get; set; }
        public int Status { get; set; }
    }
}