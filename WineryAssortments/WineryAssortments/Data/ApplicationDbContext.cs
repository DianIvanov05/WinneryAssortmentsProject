using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WineryAssortments.Data
{
    public class ApplicationDbContext : IdentityDbContext<Customer>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Wine> Wines { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<WineType> WineTypes { get; set; }
        public DbSet<WineCattegory> WineCattegories { get; set; }
        public DbSet<Promotion> Promotions { get; set; }
    }
}
