using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace scopic_test_server.Data
{
    public class UserProduct
    {
        [Key]
        public Guid UserProductId { get; set; }
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [Required]
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

    }
}