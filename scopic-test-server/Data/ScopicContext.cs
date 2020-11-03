using Microsoft.EntityFrameworkCore;

namespace scopic_test_server.Data
{
    public class ScopicContext : DbContext
    {
        public ScopicContext(DbContextOptions<ScopicContext> options) : base(options)
        {

        }
        public DbSet<Bid> Bid { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProduct> UserProducts { get; set; }

    }
}