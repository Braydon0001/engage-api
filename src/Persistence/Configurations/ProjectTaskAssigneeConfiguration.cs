namespace Engage.Persistence.Configurations;

public class ProjectTaskAssigneeConfiguration : IEntityTypeConfiguration<ProjectTaskAssignee>
{
    public void Configure(EntityTypeBuilder<ProjectTaskAssignee> builder)
    {
        builder.Property(e => e.ProjectTaskAssigneeId).IsRequired();
        builder.Property(e => e.ProjectTaskId).IsRequired();
        builder.Property(e => e.ProjectStakeholderId).IsRequired();
        builder.Property(e => e.ProjectTaskStatusId);
    }
}