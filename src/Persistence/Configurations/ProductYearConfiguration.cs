// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductYearConfiguration : IEntityTypeConfiguration<ProductYear>
{
    public void Configure(EntityTypeBuilder<ProductYear> builder)
    {
        builder.Property(e => e.ProductYearId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.EndDate).IsRequired();
    }
}