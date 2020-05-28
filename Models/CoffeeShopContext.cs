using Microsoft.EntityFrameworkCore;

namespace CoffeeShopAPI.Models
{
    public class CoffeeShopContext : DbContext
    {
        public CoffeeShopContext(DbContextOptions<CoffeeShopContext> options)
            : base(options)
        {
        }

        public DbSet<CoffeeShop> CoffeeShops { get; set; }
    }
}