using Microsoft.EntityFrameworkCore;
using Task3.Models;

namespace Task3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
