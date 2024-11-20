namespace Engage.Persistence.Configurations;

public class PaymentLineConfiguration : IEntityTypeConfiguration<PaymentLine>
{
    public void Configure(EntityTypeBuilder<PaymentLine> builder)
    {
        builder.Property(e => e.PaymentLineId).IsRequired();
        builder.Property(e => e.PaymentId).IsRequired();
        builder.Property(e => e.ExpenseTypeId).IsRequired();
        builder.Property(e => e.VatId);
        builder.Property(e => e.Amount).IsRequired();
        builder.Property(e => e.VatAmount).IsRequired();
        builder.Property(e => e.Quantity);
        builder.Property(e => e.IsVat);
        builder.Property(e => e.IsSplitAmount);
        builder.Property(e => e.HasInvoice);
        builder.Property(e => e.Note).HasMaxLength(1000);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}