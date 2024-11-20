namespace Engage.Persistence.Configurations;

public class PaymentLineDivisionConfiguration : IEntityTypeConfiguration<PaymentLineDivision>
{
    public void Configure(EntityTypeBuilder<PaymentLineDivision> builder)
    {
        builder.Property(e => e.PaymentLineId).IsRequired();
        builder.Property(e => e.EmployeeDivisionId).IsRequired();
    }
}