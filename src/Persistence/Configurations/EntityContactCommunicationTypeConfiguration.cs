namespace Engage.Persistence.Configurations;

public class EntityContactCommunicationTypeConfiguration : IEntityTypeConfiguration<EntityContactCommunicationType>
{
    public void Configure(EntityTypeBuilder<EntityContactCommunicationType> builder)
    {
        builder.Property(e => e.EntityContactCommunicationTypeId).IsRequired();
        builder.Property(e => e.EntityContactId).IsRequired();
        builder.Property(e => e.CommunicationTypeId).IsRequired();
    }
}