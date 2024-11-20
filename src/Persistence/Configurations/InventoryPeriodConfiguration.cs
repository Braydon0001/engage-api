// auto-generated
namespace Engage.Persistence.Configurations;

public class InventoryPeriodConfiguration : IEntityTypeConfiguration<InventoryPeriod>
{
    public void Configure(EntityTypeBuilder<InventoryPeriod> builder)
    {
        builder.Property(e => e.InventoryPeriodId).IsRequired();
        builder.Property(e => e.InventoryYearId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Number).IsRequired();
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.EndDate).IsRequired();

        // Multi-column indexes 

        builder.HasIndex(e => new { e.InventoryYearId, e.Number }).IsUnique();
    }
}