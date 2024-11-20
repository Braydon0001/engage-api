namespace Engage.Persistence.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.Property(e => e.PaymentId).IsRequired();
        builder.Property(e => e.PaymentBatchId).IsRequired();
        builder.Property(e => e.CreditorId).IsRequired();
        builder.Property(e => e.PaymentStatusId).IsRequired();
        builder.Property(e => e.VatId);
        builder.Property(e => e.PaymentPeriodId).IsRequired();
        builder.Property(e => e.InvoiceNumber).IsRequired().HasMaxLength(200);
        builder.Property(e => e.InvoiceDate).IsRequired();
        builder.Property(e => e.IsClaimFromSupplier);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}