using Microsoft.EntityFrameworkCore;
using RetailApp.Models.Auth;
using RetailApp.Models.SQL;

namespace RetailApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();
        public DbSet<User> Users { get; set; }
    }
}
