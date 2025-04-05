using Backend.Erp.Skeleton.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Erp.Skeleton.Infrastructure.Mappings
{
    internal class ProductsMapping : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.IdCompany).IsRequired();
            builder.Property(x => x.IdCategory).IsRequired();
            builder.Property(x => x.Status).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Image).IsRequired();

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasOne(x => x.Company)
                   .WithMany(x => x.Products)
                   .HasForeignKey(x => x.IdCompany);

            builder.HasOne(x => x.Category)
                   .WithMany(x => x.Products)
                   .HasForeignKey(x => x.IdCompany);
        }
    }
}
