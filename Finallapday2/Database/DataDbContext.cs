using Microsoft.EntityFrameworkCore; // Import EF
using WebApplication2.Models;

namespace WebApplication2.Database
{
    public class DataDbContext : DbContext
    {
        // Constructure Method
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options) { }

        // Table manufacturers
        //public DbSet<manufacturers> manufacturers { get; set; }
        ////Table devices
        //public DbSet<devices> devices { get; set; }


        // Table employees
        public DbSet<employees> employees { get; set; }
        ////Table positions
        public DbSet<positions> positions { get; set; }
    }
}
