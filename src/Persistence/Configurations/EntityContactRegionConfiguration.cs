namespace Engage.Persistence.Configurations;

public class EntityContactRegionConfiguration : IEntityTypeConfiguration<EntityContactRegion>
{
    public void Configure(EntityTypeBuilder<EntityContactRegion> builder)
    {
        builder.Property(e => e.EntityContactRegionId).IsRequired();
        builder.Property(e => e.EntityContactId).IsRequired();
        builder.Property(e => e.EngageRegionId).IsRequired();
    }
}