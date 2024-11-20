namespace Engage.Persistence.Configurations;

public class PaymentYearConfiguration : IEntityTypeConfiguration<PaymentYear>
{
    public void Configure(EntityTypeBuilder<PaymentYear> builder)
    {
        builder.Property(e => e.PaymentYearId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.EndDate).IsRequired();
    }
}