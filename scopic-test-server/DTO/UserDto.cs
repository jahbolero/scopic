using System;

namespace scopic_test_server.DTO
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; } //0-Admin,1-User
    }
}