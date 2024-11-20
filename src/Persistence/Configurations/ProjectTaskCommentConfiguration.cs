namespace Engage.Persistence.Configurations;

public class ProjectTaskCommentConfiguration : IEntityTypeConfiguration<ProjectTaskComment>
{
    public void Configure(EntityTypeBuilder<ProjectTaskComment> builder)
    {
        builder.Property(e => e.ProjectTaskCommentId).IsRequired();
        builder.Property(e => e.ProjectTaskId).IsRequired();
        builder.Property(e => e.Comment).IsRequired().HasMaxLength(1000);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}