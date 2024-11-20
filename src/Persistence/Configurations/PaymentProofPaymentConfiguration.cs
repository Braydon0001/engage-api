namespace Engage.Persistence.Configurations;

public class PaymentProofPaymentConfiguration : IEntityTypeConfiguration<PaymentProofPayment>
{
    public void Configure(EntityTypeBuilder<PaymentProofPayment> builder)
    {
        builder.Property(e => e.PaymentProofPaymentId).IsRequired();
        builder.Property(e => e.PaymentId).IsRequired();
        builder.Property(e => e.PaymentProofId).IsRequired();
    }
}