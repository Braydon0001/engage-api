// auto-generated
namespace Engage.Persistence.Configurations;

public class SurveyEngageRegionTargetConfiguration : IEntityTypeConfiguration<SurveyEngageRegionTarget>
{
    public void Configure(EntityTypeBuilder<SurveyEngageRegionTarget> builder)
    {
        builder.Property(e => e.EngageRegionId).IsRequired();

        // Multi-column indexes 

        builder.HasIndex(e => new { e.SurveyId, e.EngageRegionId }).IsUnique();
    }
}