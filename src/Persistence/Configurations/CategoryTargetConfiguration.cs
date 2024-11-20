namespace Engage.Persistence.Configurations;

public class CategoryTargetConfiguration : IEntityTypeConfiguration<CategoryTarget>
{
    public void Configure(EntityTypeBuilder<CategoryTarget> builder)
    {
        builder.Property(e => e.CategoryTargetId).IsRequired();
        builder.Property(e => e.SupplierId).IsRequired();
        builder.Property(e => e.Target).IsRequired();
        builder.Property(e => e.CategoryTargetTypeId);
        builder.Property(e => e.AvailableLabel).IsRequired().HasMaxLength(100);
        builder.Property(e => e.OccupiedLabel).IsRequired().HasMaxLength(100);
    }
}