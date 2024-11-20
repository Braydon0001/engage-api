namespace Engage.Persistence.Configurations;

public class ProjectStoreAssetConfiguration : IEntityTypeConfiguration<ProjectStoreAsset>
{
    public void Configure(EntityTypeBuilder<ProjectStoreAsset> builder)
    {
        builder.Property(e => e.ProjectStoreAssetId).IsRequired();
        builder.Property(e => e.ProjectId).IsRequired();
        builder.Property(e => e.StoreAssetId).IsRequired();
    }
}