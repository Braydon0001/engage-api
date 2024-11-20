// auto-generated
namespace Engage.Persistence.Configurations;

public class InventoryStatusConfiguration : IEntityTypeConfiguration<InventoryStatus>
{
    public void Configure(EntityTypeBuilder<InventoryStatus> builder)
    {
        builder.Property(e => e.InventoryStatusId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}