using System;
using scopic_test_server.Data;
using scopic_test_server.DTO;

namespace scopic_test_server.Interface
{
    public interface IUserRepository
    {
        User GetUser(Guid UserId);
        User Authenticate(string Username, string Password);
        UserProfileDto GetUserProfile(Guid UserId);
    }
}