namespace Engage.Persistence.Configurations;

public class PaymentLineFileTypeConfiguration : IEntityTypeConfiguration<PaymentLineFileType>
{
    public void Configure(EntityTypeBuilder<PaymentLineFileType> builder)
    {
        builder.Property(e => e.PaymentLineFileTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}