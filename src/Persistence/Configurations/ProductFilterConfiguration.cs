namespace Engage.Persistence.Configurations;

public class ProductFilterConfiguration : IEntityTypeConfiguration<ProductFilter>
{
    public void Configure(EntityTypeBuilder<ProductFilter> builder)
    {
        builder.Property(e => e.Filter)
               .IsRequired()
               .HasMaxLength(120);
    }
}
