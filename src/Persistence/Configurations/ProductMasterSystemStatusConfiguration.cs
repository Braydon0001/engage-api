// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductMasterSystemStatusConfiguration : IEntityTypeConfiguration<ProductMasterSystemStatus>
{
    public void Configure(EntityTypeBuilder<ProductMasterSystemStatus> builder)
    {
        builder.Property(e => e.ProductMasterSystemStatusId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}