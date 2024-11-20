namespace Engage.Persistence.Configurations;

public class ProjectExternalUserCommunicationTypeConfiguration : IEntityTypeConfiguration<ProjectExternalUserCommunicationType>
{
    public void Configure(EntityTypeBuilder<ProjectExternalUserCommunicationType> builder)
    {
        builder.Property(e => e.ProjectExternalUserCommunicationTypeId).IsRequired();
        builder.Property(e => e.ProjectExternalUserId).IsRequired();
        builder.Property(e => e.CommunicationTypeId).IsRequired();
    }
}