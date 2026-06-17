using Microsoft.EntityFrameworkCore;
using Warehouse.Shared.Models;

namespace InventoryService
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }
        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stock>().HasKey(s => s.MaterialNo); // Đặt ID là mã tồn kho
        }
    }
}
