using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class VatConfiguration : IEntityTypeConfiguration<Vat>
    {
        public void Configure(EntityTypeBuilder<Vat> builder)
        {
            builder.HasIndex(e => e.Code)
                   .IsUnique();

            builder.Property(e => e.Code)
                   .IsRequired()
                   .HasMaxLength(10);
            
            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(120);
        }
    }
}
