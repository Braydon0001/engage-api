namespace Engage.Persistence.Configurations;

public class SupplierSubRegionConfiguration : IEntityTypeConfiguration<SupplierSubRegion>
{
    public void Configure(EntityTypeBuilder<SupplierSubRegion> builder)
    {
        builder.Property(e => e.SupplierSubRegionId).IsRequired();
        builder.Property(e => e.SupplierRegionId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}