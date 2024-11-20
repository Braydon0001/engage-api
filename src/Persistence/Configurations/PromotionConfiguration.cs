using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
    {
        public void Configure(EntityTypeBuilder<Promotion> builder)
        {
            builder.Property(e => e.Title)
             .IsRequired()
             .HasMaxLength(220);

            builder.Property(e => e.Note)
                .HasMaxLength(300);
        }
    }
}
