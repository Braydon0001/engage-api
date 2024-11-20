// auto-generated
namespace Engage.Persistence.Configurations;

public class WebFileEngageRegionConfiguration : IEntityTypeConfiguration<WebFileEngageRegion>
{
    public void Configure(EntityTypeBuilder<WebFileEngageRegion> builder)
    {
        builder.Property(e => e.EngageRegionId).IsRequired();

        // Multi-column indexes 

        builder.HasIndex(e => new { e.WebFileId, e.EngageRegionId }).IsUnique();
    }
}