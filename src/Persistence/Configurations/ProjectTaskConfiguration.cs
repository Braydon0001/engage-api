namespace Engage.Persistence.Configurations;

public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
{
    public void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        builder.Property(e => e.ProjectTaskId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Note).HasMaxLength(1000);
        builder.Property(e => e.ProjectId).IsRequired();
        builder.Property(e => e.ProjectTaskTypeId);
        builder.Property(e => e.ProjectTaskStatusId).IsRequired();
        builder.Property(e => e.ProjectTaskPriorityId);
        builder.Property(e => e.UserId);
        builder.Property(e => e.StartDate);
        builder.Property(e => e.EndDate);
        builder.Property(e => e.EstimatedHours);
        builder.Property(e => e.RemainingHours);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}