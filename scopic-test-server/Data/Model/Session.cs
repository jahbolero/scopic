using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace scopic_test_server.Data
{
    public class Session
    {
        [Key]
        public Guid SessionId { get; set; }
        public bool IsActive { get; set; }
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}