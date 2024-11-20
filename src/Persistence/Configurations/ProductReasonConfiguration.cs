// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductReasonConfiguration : IEntityTypeConfiguration<ProductReason>
{
    public void Configure(EntityTypeBuilder<ProductReason> builder)
    {
        builder.Property(e => e.ProductReasonId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}