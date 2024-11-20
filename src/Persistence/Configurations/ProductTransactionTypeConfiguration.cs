// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductTransactionTypeConfiguration : IEntityTypeConfiguration<ProductTransactionType>
{
    public void Configure(EntityTypeBuilder<ProductTransactionType> builder)
    {
        builder.Property(e => e.ProductTransactionTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.IsPositive).IsRequired();
    }
}