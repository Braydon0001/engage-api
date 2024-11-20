namespace Engage.Persistence.Configurations;

public class PaymentPeriodConfiguration : IEntityTypeConfiguration<PaymentPeriod>
{
    public void Configure(EntityTypeBuilder<PaymentPeriod> builder)
    {
        builder.Property(e => e.PaymentPeriodId).IsRequired();
        builder.Property(e => e.PaymentYearId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Number).IsRequired();
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.EndDate).IsRequired();

        // Multi-column indexes 

        builder.HasIndex(e => new { e.PaymentYearId, e.Number }).IsUnique();
    }
}