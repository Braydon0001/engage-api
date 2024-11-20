namespace Engage.Persistence.Configurations;

public class ProjectTaskTypeConfiguration : IEntityTypeConfiguration<ProjectTaskType>
{
    public void Configure(EntityTypeBuilder<ProjectTaskType> builder)
    {
        builder.Property(e => e.ProjectTaskTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}