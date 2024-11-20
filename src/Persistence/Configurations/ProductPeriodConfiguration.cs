// auto-generated
namespace Engage.Persistence.Configurations;

public class ProductPeriodConfiguration : IEntityTypeConfiguration<ProductPeriod>
{
    public void Configure(EntityTypeBuilder<ProductPeriod> builder)
    {
        builder.Property(e => e.ProductPeriodId).IsRequired();
        builder.Property(e => e.ProductYearId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.EndDate).IsRequired();

        // Multi-column indexes 

        builder.HasIndex(e => new { e.ProductYearId, e.Name }).IsUnique();
    }
}