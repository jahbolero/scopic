using System;

namespace scopic_test_server.Data
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; } //0-Admin,1-User
    }
}