namespace Engage.Persistence.Configurations;

public class PaymentStatusHistoryConfiguration : IEntityTypeConfiguration<PaymentStatusHistory>
{
    public void Configure(EntityTypeBuilder<PaymentStatusHistory> builder)
    {
        builder.Property(e => e.PaymentStatusHistoryId).IsRequired();
        builder.Property(e => e.PaymentId).IsRequired();
        builder.Property(e => e.PaymentStatusId).IsRequired();
        builder.Property(e => e.Reason).HasMaxLength(300);
    }
}