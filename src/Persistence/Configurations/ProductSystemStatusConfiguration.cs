// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductSystemStatusConfiguration : IEntityTypeConfiguration<ProductSystemStatus>
{
    public void Configure(EntityTypeBuilder<ProductSystemStatus> builder)
    {
        builder.Property(e => e.ProductSystemStatusId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}