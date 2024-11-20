using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class ClaimYearConfiguration : IEntityTypeConfiguration<ClaimYear>
    {
        public void Configure(EntityTypeBuilder<ClaimYear> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}
