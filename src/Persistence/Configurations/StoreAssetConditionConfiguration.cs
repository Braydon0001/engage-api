// auto-generated
namespace Engage.Persistence.Configurations;

public class StoreAssetConditionConfiguration : IEntityTypeConfiguration<StoreAssetCondition>
{
    public void Configure(EntityTypeBuilder<StoreAssetCondition> builder)
    {
        builder.Property(e => e.StoreAssetConditionId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}