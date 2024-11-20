namespace Engage.Persistence.Configurations;

public class ProductOrderLineStatusConfiguration : IEntityTypeConfiguration<ProductOrderLineStatus>
{
    public void Configure(EntityTypeBuilder<ProductOrderLineStatus> builder)
    {
        builder.Property(e => e.ProductOrderLineStatusId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}