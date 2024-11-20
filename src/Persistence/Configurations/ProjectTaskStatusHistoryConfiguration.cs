namespace Engage.Persistence.Configurations;

public class ProjectTaskStatusHistoryConfiguration : IEntityTypeConfiguration<ProjectTaskStatusHistory>
{
    public void Configure(EntityTypeBuilder<ProjectTaskStatusHistory> builder)
    {
        builder.Property(e => e.ProjectTaskStatusHistoryId).IsRequired();
        builder.Property(e => e.ProjectTaskId).IsRequired();
        builder.Property(e => e.ProjectTaskStatusId).IsRequired();
        builder.Property(e => e.Reason).HasMaxLength(300);
    }
}