using BootCamp_WebAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace BootCamp_WebAPI.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Product> products { get; set; }
    }
}
