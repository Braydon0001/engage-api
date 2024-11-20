// auto-generated
namespace Engage.Persistence.Configurations;

public class PayrollPeriodConfiguration : IEntityTypeConfiguration<PayrollPeriod>
{
    public void Configure(EntityTypeBuilder<PayrollPeriod> builder)
    {
        builder.Property(e => e.PayrollPeriodId).IsRequired();
        builder.Property(e => e.PayrollYearId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Number).IsRequired();
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.EndDate).IsRequired();

        // Multi-column indexes 

        builder.HasIndex(e => new { e.PayrollYearId, e.Number }).IsUnique();
    }
}