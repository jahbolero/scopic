using System;
using System.Linq;
using scopic_test_server.DTO;
using scopic_test_server.Interface;

namespace scopic_test_server.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ScopicContext _context;
        public UserRepository(ScopicContext context)
        {
            _context = context;
        }
        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.Users.SingleOrDefault(x => x.Username == username);
            if (user == null)
                return null;
            if (password != user.Password)
                return null;
            return user;
        }

        public User GetUser(Guid UserId)
        {
            return _context.Users.FirstOrDefault(x => x.UserId == UserId);
        }
    }
}