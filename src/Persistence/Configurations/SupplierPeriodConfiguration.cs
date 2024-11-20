namespace Engage.Persistence.Configurations;

public class SupplierPeriodConfiguration : IEntityTypeConfiguration<SupplierPeriod>
{
    public void Configure(EntityTypeBuilder<SupplierPeriod> builder)
    {
        builder.Property(e => e.SupplierPeriodId).IsRequired();
        builder.Property(e => e.SupplierYearId).IsRequired();
        builder.Property(e => e.Number).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.EndDate).IsRequired();
    }
}
