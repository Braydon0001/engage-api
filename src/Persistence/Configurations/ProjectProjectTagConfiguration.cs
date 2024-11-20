namespace Engage.Persistence.Configurations;

public class ProjectProjectTagConfiguration : IEntityTypeConfiguration<ProjectProjectTag>
{
    public void Configure(EntityTypeBuilder<ProjectProjectTag> builder)
    {
        builder.Property(e => e.ProjectProjectTagId).IsRequired();
        builder.Property(e => e.ProjectId).IsRequired();
    }
}