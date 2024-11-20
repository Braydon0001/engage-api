namespace Engage.Persistence.Configurations;

public class ProjectCampaignConfiguration : IEntityTypeConfiguration<ProjectCampaign>
{
    public void Configure(EntityTypeBuilder<ProjectCampaign> builder)
    {
        builder.Property(e => e.ProjectCampaignId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Note).HasColumnType("json");
        builder.Property(e => e.EngageRegionId);
        builder.Property(e => e.StartDate);
        builder.Property(e => e.EndDate);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}