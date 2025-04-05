using Backend.Erp.Skeleton.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Erp.Skeleton.Infrastructure.Mappings
{
    internal class PersonsMapping : IEntityTypeConfiguration<Persons>
    {
        public void Configure(EntityTypeBuilder<Persons> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Cpf).IsRequired().HasMaxLength(11);
            builder.Property(x => x.IdUser).IsRequired();
            builder.Property(x => x.IdUserType).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);

            builder.HasIndex(x => x.Cpf).IsUnique();

            builder.HasOne(x => x.Company)
                   .WithMany(x => x.Persons)
                   .HasForeignKey(x => x.IdCompany)
                   .IsRequired(false);
        }
    }
}
