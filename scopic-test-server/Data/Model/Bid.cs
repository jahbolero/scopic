using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace scopic_test_server.Data
{
    public class Bid
    {
        [Key]
        public Guid BidId { get; set; }
        [Required]
        public decimal BidAmount { get; set; }
        [Required]
        public DateTime BidDate { get; set; }
        [Required]
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}