using BootCamp24_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BootCamp_WebAPI.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<ProductModel> products { get; set; }
    }
}
