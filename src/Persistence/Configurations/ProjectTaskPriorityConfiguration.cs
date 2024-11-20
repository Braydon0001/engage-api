namespace Engage.Persistence.Configurations;

public class ProjectTaskPriorityConfiguration : IEntityTypeConfiguration<ProjectTaskPriority>
{
    public void Configure(EntityTypeBuilder<ProjectTaskPriority> builder)
    {
        builder.Property(e => e.ProjectTaskPriorityId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}