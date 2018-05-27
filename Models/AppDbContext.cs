
using Microsoft.EntityFrameworkCore;
namespace MyBrands.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public virtual DbSet<Brands> Brands { get; set; }
        public virtual DbSet<Product> Products{ get; set; }
    }
}