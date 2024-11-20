// auto-generated
namespace Engage.Persistence.Configurations;

public class PayrollYearConfiguration : IEntityTypeConfiguration<PayrollYear>
{
    public void Configure(EntityTypeBuilder<PayrollYear> builder)
    {
        builder.Property(e => e.PayrollYearId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.EndDate).IsRequired();
    }
}