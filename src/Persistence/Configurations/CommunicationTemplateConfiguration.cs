namespace Engage.Persistence.Configurations;

public class CommunicationTemplateConfiguration : IEntityTypeConfiguration<CommunicationTemplate>
{
    public void Configure(EntityTypeBuilder<CommunicationTemplate> builder)
    {
        builder.Property(e => e.CommunicationTemplateId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.CommunicationTemplateTypeId).IsRequired();
        builder.Property(e => e.CommunicationTypeId).IsRequired();
        builder.Property(e => e.ExternalTemplateId).HasMaxLength(200);
        builder.Property(e => e.FromName).HasMaxLength(200);
        builder.Property(e => e.FromEmailAddress).HasMaxLength(200);
        builder.Property(e => e.FromMobileNumber).HasMaxLength(15);
        builder.Property(e => e.Subject).HasMaxLength(200);
        builder.Property(e => e.Body).HasMaxLength(1000);
    }
}