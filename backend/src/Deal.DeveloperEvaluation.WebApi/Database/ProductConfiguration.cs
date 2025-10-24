using Deal.DeveloperEvaluation.WebApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Deal.DeveloperEvaluation.WebApi.Database
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
               .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasColumnName("Name")
                .HasConversion(
                    x => x.Value,
                    n => new ProductName(n))
                .IsRequired();

            builder.HasIndex(p => p.Code).IsUnique();
            builder.Property(p => p.Code)
                .HasColumnName("Sku")
                .HasConversion(
                    x => x.Value,
                    s => new Sku(s))
                .IsRequired();

            builder.Property(p => p.Price)
                .HasColumnName("Price")
                .HasConversion(
                    x => x.Value,
                    p => new Money(p))
                .IsRequired();
        }
    }
}
