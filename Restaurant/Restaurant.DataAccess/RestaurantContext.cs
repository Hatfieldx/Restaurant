using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using Microsoft.Extensions.Logging;
using custom_logger.Logger;

namespace Restaurant.DataAccess
{
    public class RestaurantContext : DbContext
    {
        public static ILoggerFactory logger = LoggerFactory.Create(builder =>
        {
            builder
            .AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information)
            .AddProvider(new DbLoggerProvider(@"log.txt"));        
        });
        public DbSet<Category> Categories { get; set; }
        public DbSet<FoodType> FoodTypes { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }

        public RestaurantContext(DbContextOptions<RestaurantContext> options)
            : base(options)
        {       
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(logger);
        }
    }
}
