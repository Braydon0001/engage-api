namespace Engage.Persistence.Configurations;

public class UserCommunicationTypeConfiguration : IEntityTypeConfiguration<UserCommunicationType>
{
    public void Configure(EntityTypeBuilder<UserCommunicationType> builder)
    {
        builder.Property(e => e.UserCommunicationTypeId).IsRequired();
        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.CommunicationTypeId).IsRequired();
    }
}