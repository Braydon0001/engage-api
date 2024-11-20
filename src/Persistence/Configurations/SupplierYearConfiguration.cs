namespace Engage.Persistence.Configurations;

public class SupplierYearConfiguration : IEntityTypeConfiguration<SupplierYear>
{
    public void Configure(EntityTypeBuilder<SupplierYear> builder)
    {
        builder.Property(e => e.SupplierYearId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}
