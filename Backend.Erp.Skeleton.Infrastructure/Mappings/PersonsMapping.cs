using Backend.Erp.Skeleton.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Erp.Skeleton.Infrastructure.Mappings
{
    internal class PersonsMapping : IEntityTypeConfiguration<Persons>
    {
        public void Configure(EntityTypeBuilder<Persons> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).ValueGeneratedOnAdd();

            builder.Property(x => x.cpf).IsRequired().HasMaxLength(11);
            builder.Property(x => x.idUser).IsRequired();
            builder.Property(x => x.IdUserType).IsRequired();
            builder.Property(x => x.name).IsRequired().HasMaxLength(100);

            builder.HasIndex(x => x.cpf).IsUnique();

            builder.HasOne(x => x.company)
                   .WithMany(x => x.Persons)
                   .HasForeignKey(x => x.idCompany)
                   .IsRequired(false);
        }
    }
}
