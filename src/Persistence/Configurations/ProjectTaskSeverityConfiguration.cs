namespace Engage.Persistence.Configurations;

public class ProjectTaskSeverityConfiguration : IEntityTypeConfiguration<ProjectTaskSeverity>
{
    public void Configure(EntityTypeBuilder<ProjectTaskSeverity> builder)
    {
        builder.Property(e => e.ProjectTaskSeverityId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}