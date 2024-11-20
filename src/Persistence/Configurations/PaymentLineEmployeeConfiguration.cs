namespace Engage.Persistence.Configurations;

public class PaymentLineEmployeeConfiguration : IEntityTypeConfiguration<PaymentLineEmployee>
{
    public void Configure(EntityTypeBuilder<PaymentLineEmployee> builder)
    {
        builder.Property(e => e.PaymentLineId).IsRequired();
        builder.Property(e => e.EmployeeId).IsRequired();
    }
}