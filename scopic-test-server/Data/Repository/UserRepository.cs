using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using scopic_test_server.DTO;
using scopic_test_server.Interface;

namespace scopic_test_server.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ScopicContext _context;
        private readonly IMapper _mapper;
        public UserRepository(ScopicContext context, IMapper mapper)
        {
            _mapper = mapper;
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

        public UserProfileDto GetUserProfile(Guid UserId)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == UserId);
            var productsBidOn = _context.Bid.Where(x => x.UserId == user.UserId).Include("Product.UserProduct").Include("Product.Bids").Select(y => y.Product).Distinct();
            var userProfile = new UserProfileDto()
            {
                User = _mapper.Map<UserDto>(user),
                ProductsBidOn = _mapper.Map<IEnumerable<ProductReadDto>>(productsBidOn)
            };

            return userProfile;

        }

        public User Register(string Username, string Password)
        {
            var existingUser = _context.Users.FirstOrDefault(x => x.Username == Username);
            if (existingUser != null)
                return null;
            var user = new User()
            {
                UserId = Guid.NewGuid(),
                Username = Username,
                Password = Password,
                Role = Role.User
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
    }
}