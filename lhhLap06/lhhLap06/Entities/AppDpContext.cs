using Microsoft.EntityFrameworkCore;
using lhhLap06.Models;
namespace lhhLap06.Entities
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<lhhCategory> Categories { get; set; }
        public DbSet<lhhProduct> Products { get; set; }
    }
}
