// auto-generated
namespace Engage.Persistence.Configurations;

public class InventoryTransactionStatusConfiguration : IEntityTypeConfiguration<InventoryTransactionStatus>
{
    public void Configure(EntityTypeBuilder<InventoryTransactionStatus> builder)
    {
        builder.Property(e => e.InventoryTransactionStatusId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}