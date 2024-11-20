namespace Engage.Persistence.Configurations;

public class CommunicationHistoryConfiguration : IEntityTypeConfiguration<CommunicationHistory>
{
    public void Configure(EntityTypeBuilder<CommunicationHistory> builder)
    {
        builder.Property(e => e.CommunicationHistoryId).IsRequired();
        builder.Property(e => e.CommunicationTemplateId).IsRequired();
        builder.Property(e => e.ToEmail).IsRequired().HasMaxLength(200);
        builder.Property(e => e.FromEmail).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Subject).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Body).IsRequired().HasMaxLength(10000);
        builder.Property(e => e.FromName).HasMaxLength(200);
        builder.Property(e => e.CcEmails).HasMaxLength(1000);
        builder.Property(e => e.AttachmentUrls).HasMaxLength(1000);
        builder.Property(e => e.Error).HasMaxLength(1000);

    }
}