using Microsoft.EntityFrameworkCore;
using Warehouse.Shared.Models;

namespace MaterialService
{
    public class MaterialDbContext : DbContext
    {
        public MaterialDbContext(DbContextOptions<MaterialDbContext> options) : base(options) { }
        public DbSet<Material> Materials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Material>().HasKey(m => m.MaterialNo); // Đặt ID là mã vật liệu
        }
    }
}
