using Engage.Domain.Entities.LinkEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class EngageSubGroupEngageBrandConfiguration : IEntityTypeConfiguration<EngageSubGroupEngageBrand>
    {
        public void Configure(EntityTypeBuilder<EngageSubGroupEngageBrand> builder)
        {
            builder.HasKey(e => new { e.EngageSubGroupId, e.EngageBrandId });

            builder.HasOne(x => x.EngageSubGroup)
                .WithMany(e => e.EngageBrands)
                .HasForeignKey(x => x.EngageSubGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.EngageBrand)
                .WithMany(e => e.EngageSubGroups)
                .HasForeignKey(x => x.EngageBrandId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
