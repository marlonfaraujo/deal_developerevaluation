using Deal.DeveloperEvaluation.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Deal.DeveloperEvaluation.WebApi.Database
{
    public class DefaultContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();

        public DefaultContext(DbContextOptions<DefaultContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
