namespace Engage.Persistence.Configurations;

public class ProjectAssigneeConfiguration : IEntityTypeConfiguration<ProjectAssignee>
{
    public void Configure(EntityTypeBuilder<ProjectAssignee> builder)
    {
        builder.Property(e => e.ProjectAssigneeId).IsRequired();
        builder.Property(e => e.ProjectId).IsRequired();
        builder.Property(e => e.ProjectStakeholderId).IsRequired();
        builder.Property(e => e.ProjectStatusId);
    }
}