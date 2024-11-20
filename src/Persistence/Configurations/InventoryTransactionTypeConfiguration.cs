// auto-generated
namespace Engage.Persistence.Configurations;

public class InventoryTransactionTypeConfiguration : IEntityTypeConfiguration<InventoryTransactionType>
{
    public void Configure(EntityTypeBuilder<InventoryTransactionType> builder)
    {
        builder.Property(e => e.InventoryTransactionTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.IsPositive);
    }
}