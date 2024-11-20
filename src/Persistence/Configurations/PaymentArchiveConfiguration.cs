namespace Engage.Persistence.Configurations;

public class PaymentArchiveConfiguration : IEntityTypeConfiguration<PaymentArchive>
{
    public void Configure(EntityTypeBuilder<PaymentArchive> builder)
    {
        builder.Property(e => e.PaymentArchiveId).IsRequired();
        builder.Property(e => e.ArchiveDate).IsRequired();
        builder.Property(e => e.Files).HasColumnType("json");
    }
}