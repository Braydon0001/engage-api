// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductModuleStatusConfiguration : IEntityTypeConfiguration<ProductModuleStatus>
{
    public void Configure(EntityTypeBuilder<ProductModuleStatus> builder)
    {
        builder.Property(e => e.ProductModuleStatusId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}