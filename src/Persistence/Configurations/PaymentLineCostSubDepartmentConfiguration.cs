namespace Engage.Persistence.Configurations;

public class PaymentLineCostSubDepartmentConfiguration : IEntityTypeConfiguration<PaymentLineCostSubDepartment>
{
    public void Configure(EntityTypeBuilder<PaymentLineCostSubDepartment> builder)
    {
        builder.Property(e => e.PaymentLineId).IsRequired();
        builder.Property(e => e.CostSubDepartmentId).IsRequired();
    }
}