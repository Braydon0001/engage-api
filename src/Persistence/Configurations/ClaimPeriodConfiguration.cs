using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    class ClaimPeriodConfiguration : IEntityTypeConfiguration<ClaimPeriod>
    {
        public void Configure(EntityTypeBuilder<ClaimPeriod> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
