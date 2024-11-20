namespace Engage.Persistence.Configurations;

public class PaymentBatchConfiguration : IEntityTypeConfiguration<PaymentBatch>
{
    public void Configure(EntityTypeBuilder<PaymentBatch> builder)
    {
        builder.Property(e => e.PaymentBatchId).IsRequired();
        builder.Property(e => e.BatchDate).IsRequired();
    }
}