// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductTransactionStatusConfiguration : IEntityTypeConfiguration<ProductTransactionStatus>
{
    public void Configure(EntityTypeBuilder<ProductTransactionStatus> builder)
    {
        builder.Property(e => e.ProductTransactionStatusId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}