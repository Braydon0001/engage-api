namespace Engage.Persistence.Configurations;

public class ProjectCommentConfiguration : IEntityTypeConfiguration<ProjectComment>
{
    public void Configure(EntityTypeBuilder<ProjectComment> builder)
    {
        builder.Property(e => e.ProjectCommentId).IsRequired();
        builder.Property(e => e.ProjectId).IsRequired();
        builder.Property(e => e.Comment).IsRequired().HasMaxLength(1000);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}