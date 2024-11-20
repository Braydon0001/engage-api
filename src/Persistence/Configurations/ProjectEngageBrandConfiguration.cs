namespace Engage.Persistence.Configurations;

public class ProjectEngageBrandConfiguration : IEntityTypeConfiguration<ProjectEngageBrand>
{
    public void Configure(EntityTypeBuilder<ProjectEngageBrand> builder)
    {
        builder.Property(e => e.ProjectEngageBrandId).IsRequired();
        builder.Property(e => e.ProjectId).IsRequired();
        builder.Property(e => e.EngageBrandId).IsRequired();
    }
}