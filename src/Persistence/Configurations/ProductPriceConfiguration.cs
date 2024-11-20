namespace Engage.Persistence.Configurations;

public class ProductPriceConfiguration : IEntityTypeConfiguration<ProductPrice>
{
    public void Configure(EntityTypeBuilder<ProductPrice> builder)
    {
        builder.Property(e => e.ProductPriceId).IsRequired();
        builder.Property(e => e.ProductId).IsRequired();
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.Price).IsRequired();
    }
}