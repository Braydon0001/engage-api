namespace Engage.Persistence.Configurations;

public class ProductOrderStatusConfiguration : IEntityTypeConfiguration<ProductOrderStatus>
{
    public void Configure(EntityTypeBuilder<ProductOrderStatus> builder)
    {
        builder.Property(e => e.ProductOrderStatusId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}