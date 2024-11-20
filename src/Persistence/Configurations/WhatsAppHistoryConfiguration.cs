namespace Engage.Persistence.Configurations;

public class WhatsAppHistoryConfiguration : IEntityTypeConfiguration<WhatsAppHistory>
{
    public void Configure(EntityTypeBuilder<WhatsAppHistory> builder)
    {
        builder.Property(e => e.WhatsAppHistoryId).IsRequired();
        builder.Property(e => e.ToMobileNumber).IsRequired().HasMaxLength(20);
        builder.Property(e => e.FromMobileNumber).IsRequired().HasMaxLength(20);
        builder.Property(e => e.ContentVariables).HasMaxLength(1000);
        builder.Property(e => e.Message).HasMaxLength(1000);
        builder.Property(e => e.FromName).HasMaxLength(200);
        builder.Property(e => e.ExternalTemplateId).HasMaxLength(100);
        builder.Property(e => e.AttachmentUrls).HasMaxLength(1000);
        builder.Property(e => e.Error).HasMaxLength(1000);

    }
}