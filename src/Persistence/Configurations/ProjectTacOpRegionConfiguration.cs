namespace Engage.Persistence.Configurations;

public class ProjectTacOpRegionConfiguration : IEntityTypeConfiguration<ProjectTacOpRegion>
{
    public void Configure(EntityTypeBuilder<ProjectTacOpRegion> builder)
    {
        builder.HasKey(e => new { e.ProjectTacOpId, e.EngageRegionId })
            .IsClustered(false);

        builder.HasOne(x => x.ProjectTacOp)
            .WithMany(c => c.ProjectTacOpRegions)
            .HasForeignKey(x => x.ProjectTacOpId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.EngageRegion)
            .WithMany(c => c.ProjectTacOpRegions)
            .HasForeignKey(x => x.EngageRegionId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}