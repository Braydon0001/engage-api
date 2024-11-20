namespace Engage.Persistence.Configurations;

public class PaymentLineCostCenterConfiguration : IEntityTypeConfiguration<PaymentLineCostCenter>
{
    public void Configure(EntityTypeBuilder<PaymentLineCostCenter> builder)
    {
        builder.Property(e => e.PaymentLineId).IsRequired();
        builder.Property(e => e.CostCenterId).IsRequired();
    }
}