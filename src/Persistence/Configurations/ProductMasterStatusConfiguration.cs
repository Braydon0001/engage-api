// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductMasterStatusConfiguration : IEntityTypeConfiguration<ProductMasterStatus>
{
    public void Configure(EntityTypeBuilder<ProductMasterStatus> builder)
    {
        builder.Property(e => e.ProductMasterStatusId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}