using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace scopic_test_server.Data
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        [MaxLength(30)]
        public string ProductName { get; set; }
        [MaxLength(200)]
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        public string ImgUrl { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public DateTime UploadDate { get; set; }
        public int Status { get; set; }
        public IEnumerable<Bid> Bids { get; set; }
        public UserProduct UserProduct  { get; set; }
    }
}