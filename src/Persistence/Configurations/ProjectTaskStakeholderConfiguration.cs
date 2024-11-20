namespace Engage.Persistence.Configurations;

public class ProjectTaskStakeholderConfiguration : IEntityTypeConfiguration<ProjectTaskStakeholder>
{
    public void Configure(EntityTypeBuilder<ProjectTaskStakeholder> builder)
    {
        builder.Property(e => e.ProjectTaskStakeholderId).IsRequired();
        builder.Property(e => e.ProjectTaskId).IsRequired();
        builder.Property(e => e.ProjectStakeholderId).IsRequired();
        builder.Property(e => e.ProjectTaskStatusId).IsRequired();
        builder.Property(e => e.Emails);
    }
}