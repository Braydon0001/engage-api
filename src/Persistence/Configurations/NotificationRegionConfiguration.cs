using Engage.Domain.Entities.LinkEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class NotificationRegionConfiguration : IEntityTypeConfiguration<NotificationRegion>
    {
        public void Configure(EntityTypeBuilder<NotificationRegion> builder)
        {
            builder.HasKey(e => new { e.NotificationId, e.EngageRegionId })
                .IsClustered(false);

            builder.HasOne(x => x.Notification)
                .WithMany(c => c.NotificationRegions)
                .HasForeignKey(x => x.NotificationId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.EngageRegion)
                .WithMany(c => c.NotificationRegions)
                .HasForeignKey(x => x.EngageRegionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
