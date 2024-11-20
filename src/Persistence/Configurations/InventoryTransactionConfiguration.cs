// auto-generated
namespace Engage.Persistence.Configurations;

public class InventoryTransactionConfiguration : IEntityTypeConfiguration<InventoryTransaction>
{
    public void Configure(EntityTypeBuilder<InventoryTransaction> builder)
    {
        builder.Property(e => e.InventoryTransactionId).IsRequired();
        builder.Property(e => e.InventoryTransactionTypeId).IsRequired();
        builder.Property(e => e.InventoryTransactionStatusId).IsRequired();
        builder.Property(e => e.InventoryId).IsRequired();
        builder.Property(e => e.InventoryWarehouseId).IsRequired();
        builder.Property(e => e.Quantity).IsRequired();
        builder.Property(e => e.TransactionDate).IsRequired();
        builder.Property(e => e.Note).HasMaxLength(1000);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}