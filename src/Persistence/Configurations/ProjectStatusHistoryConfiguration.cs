namespace Engage.Persistence.Configurations;

public class ProjectStatusHistoryConfiguration : IEntityTypeConfiguration<ProjectStatusHistory>
{
    public void Configure(EntityTypeBuilder<ProjectStatusHistory> builder)
    {
        builder.Property(e => e.ProjectStatusHistoryId).IsRequired();
        builder.Property(e => e.ProjectId).IsRequired();
        builder.Property(e => e.ProjectStatusId).IsRequired();
        builder.Property(e => e.Reason).HasMaxLength(300);
    }
}