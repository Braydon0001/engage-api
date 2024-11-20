namespace Engage.Persistence.Configurations;

public class PaymentBatchRegionConfiguration : IEntityTypeConfiguration<PaymentBatchRegion>
{
    public void Configure(EntityTypeBuilder<PaymentBatchRegion> builder)
    {
        builder.Property(e => e.PaymentBatchId).IsRequired();
        builder.Property(e => e.EngageRegionId).IsRequired();
    }
}