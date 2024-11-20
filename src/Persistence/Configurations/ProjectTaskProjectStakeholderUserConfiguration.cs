namespace Engage.Persistence.Configurations;

public class ProjectTaskProjectStakeholderUserConfiguration : IEntityTypeConfiguration<ProjectTaskProjectStakeholderUser>
{
    public void Configure(EntityTypeBuilder<ProjectTaskProjectStakeholderUser> builder)
    {
        builder.Property(e => e.ProjectTaskProjectStakeholderUserId).IsRequired();
        builder.Property(e => e.ProjectTaskId).IsRequired();
        builder.Property(e => e.ProjectStakeholderId).IsRequired();
    }
}