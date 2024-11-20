namespace Engage.Persistence.Configurations;

public class CommunicationTemplateTypeConfiguration : IEntityTypeConfiguration<CommunicationTemplateType>
{
    public void Configure(EntityTypeBuilder<CommunicationTemplateType> builder)
    {
        builder.Property(e => e.CommunicationTemplateTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}