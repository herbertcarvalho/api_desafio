using Backend.Erp.Skeleton.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Erp.Skeleton.Infrastructure.Mappings
{
    internal class ProductsMapping : IEntityTypeConfiguration<Products>
    {
        public void Configure(EntityTypeBuilder<Products> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).ValueGeneratedOnAdd();

            builder.Property(x => x.idCompany).IsRequired();
            builder.Property(x => x.idCategory).IsRequired();
            builder.Property(x => x.status).IsRequired();
            builder.Property(x => x.name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.price).IsRequired();
            builder.Property(x => x.image).IsRequired();

            builder.HasIndex(x => x.name).IsUnique();

            builder.HasOne(x => x.Company)
                   .WithMany(x => x.Products)
                   .HasForeignKey(x => x.idCompany);

            builder.HasOne(x => x.Category)
                   .WithMany(x => x.Products)
                   .HasForeignKey(x => x.idCompany);
        }
    }
}
