namespace Engage.Persistence.Configurations;

public class InventoryYearConfiguration : IEntityTypeConfiguration<InventoryYear>
{
    public void Configure(EntityTypeBuilder<InventoryYear> builder)
    {
        builder.Property(e => e.InventoryYearId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.EndDate).IsRequired();
    }
}