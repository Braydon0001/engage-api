namespace Engage.Persistence.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.Property(e => e.ProjectId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.ProjectTypeId).IsRequired();
        builder.Property(e => e.ProjectStatusId).IsRequired();
        builder.Property(e => e.ProjectPriorityId).IsRequired();
        builder.Property(e => e.EngageRegionId);
        builder.Property(e => e.ProjectCampaignId);
        builder.Property(e => e.ProjectSubTypeId);
        builder.Property(e => e.ProjectCategoryId);
        builder.Property(e => e.ProjectSubCategoryId);
        builder.Property(e => e.OwnerId);
        builder.Property(e => e.StartDate);
        builder.Property(e => e.EndDate);
        builder.Property(e => e.EstimatedHours);
        builder.Property(e => e.RemainingHours);
        builder.Property(e => e.Emails);
        builder.Property(e => e.Note).HasColumnType("json");
        builder.Property(e => e.Files).HasColumnType("json");
    }
}