using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class ClaimSkuTypeConfiguration : IEntityTypeConfiguration<ClaimSkuType>
    {
        public void Configure(EntityTypeBuilder<ClaimSkuType> builder)
        {
            builder.Property(e => e.Name)
                   .HasMaxLength(100)
                   .IsRequired();
        }
    }
}
