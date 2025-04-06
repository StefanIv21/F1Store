using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Backend.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class ProductsConfiguration: IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(e => e.Id) 
            .IsRequired();
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name)
             .HasMaxLength(255) 
            .IsRequired();
        builder.Property(p => p.Price).IsRequired();
        builder.Property(p => p.Description)
            .HasMaxLength(255)
            .IsRequired();
       builder.Property(p => p.Type)
            .HasMaxLength(255)
            .IsRequired();
       builder.Property(p => p.Brand)
           .HasMaxLength(255)
           .IsRequired();
       builder.Property(p => p.Stock)
           .IsRequired();
    }
}