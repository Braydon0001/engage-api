namespace Engage.Persistence.Configurations;

public class ProjectTaskStatusConfiguration : IEntityTypeConfiguration<ProjectTaskStatus>
{
    public void Configure(EntityTypeBuilder<ProjectTaskStatus> builder)
    {
        builder.Property(e => e.ProjectTaskStatusId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}