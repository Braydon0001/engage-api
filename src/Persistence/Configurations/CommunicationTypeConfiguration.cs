namespace Engage.Persistence.Configurations;

public class CommunicationTypeConfiguration : IEntityTypeConfiguration<CommunicationType>
{
    public void Configure(EntityTypeBuilder<CommunicationType> builder)
    {
        builder.Property(e => e.CommunicationTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}