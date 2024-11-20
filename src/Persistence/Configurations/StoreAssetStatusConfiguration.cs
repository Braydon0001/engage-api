namespace Engage.Persistence.Configurations;

public class StoreAssetStatusConfiguration : IEntityTypeConfiguration<StoreAssetStatus>
{
    public void Configure(EntityTypeBuilder<StoreAssetStatus> builder)
    {
        builder.Property(e => e.StoreAssetStatusId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(120);
    }
}