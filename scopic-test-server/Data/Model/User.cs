using System;

namespace scopic_test_server.Data
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } //Admin,User
    }
}