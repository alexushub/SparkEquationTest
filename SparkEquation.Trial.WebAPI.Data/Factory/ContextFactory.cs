using Microsoft.EntityFrameworkCore;

namespace SparkEquation.Trial.WebAPI.Data.Factory
{
    public class ContextFactory : IContextFactory
    {
        public MainDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<MainDbContext>()
                .UseSqlite("Data Source=./../SparkEquation.Trial.WebAPI/products.db")
                .Options;

            return new MainDbContext(options);
        }
    }
}