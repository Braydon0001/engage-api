namespace Engage.Persistence.Configurations;

public class PaymentLineFileConfiguration : IEntityTypeConfiguration<PaymentLineFile>
{
    public void Configure(EntityTypeBuilder<PaymentLineFile> builder)
    {
        builder.Property(e => e.PaymentLineFileId).IsRequired();
        builder.Property(e => e.PaymentLineId).IsRequired();
        builder.Property(e => e.PaymentLineFileTypeId).IsRequired();
        builder.Property(e => e.Files).HasColumnType("json");
    }
}