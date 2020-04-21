using Microsoft.EntityFrameworkCore;
using SparkEquation.Trial.WebAPI.Data.Models;

namespace SparkEquation.Trial.WebAPI.Data
{
    public partial class MainDbContext : DbContext
    {
        public MainDbContext() : base()
        {

        }

        public MainDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .Property(m => m.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<CategoryProduct>().HasKey(cp => new { cp.CategoryId, cp.ProductId });
            SeedData(modelBuilder);
        }

        public DbSet<Brand> Brands { get;set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<CategoryProduct> CategoryProducts { get; set; }
    }
}
