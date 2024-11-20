// auto-generated
namespace Engage.Persistence.Configurations;

public class SurveyStoreTargetConfiguration : IEntityTypeConfiguration<SurveyStoreTarget>
{
    public void Configure(EntityTypeBuilder<SurveyStoreTarget> builder)
    {
        builder.Property(e => e.StoreId).IsRequired();

        // Multi-column indexes 

        builder.HasIndex(e => new { e.SurveyId, e.StoreId }).IsUnique();
    }
}