using Backend.Erp.Skeleton.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Erp.Skeleton.Infrastructure.Mappings
{
    internal class CompanyMapping : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).ValueGeneratedOnAdd();

            builder.Property(x => x.cnpj).IsRequired().HasMaxLength(14);
            builder.Property(x => x.name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.image);


            builder.HasIndex(x => x.cnpj).IsUnique();

        }
    }
}
