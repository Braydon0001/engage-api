// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductTransactionConfiguration : IEntityTypeConfiguration<ProductTransaction>
{
    public void Configure(EntityTypeBuilder<ProductTransaction> builder)
    {
        builder.Property(e => e.ProductTransactionId).IsRequired();
        builder.Property(e => e.ProductId).IsRequired();
        builder.Property(e => e.ProductTransactionTypeId).IsRequired();
        builder.Property(e => e.ProductTransactionStatusId).IsRequired();
        builder.Property(e => e.ProductPeriodId).IsRequired();
        builder.Property(e => e.ProductWarehouseId).IsRequired();
        builder.Property(e => e.EmployeeId);
        builder.Property(e => e.EngageRegionId);
        builder.Property(e => e.Quantity).IsRequired().HasDefaultValue(0);
        builder.Property(e => e.Price).IsRequired().HasDefaultValue(0);
        builder.Property(e => e.TransactionDate).IsRequired();
        builder.Property(e => e.Note).IsRequired().HasMaxLength(1000);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}