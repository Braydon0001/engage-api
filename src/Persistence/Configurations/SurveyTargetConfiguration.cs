// auto-generated
namespace Engage.Persistence.Configurations;

public class SurveyTargetConfiguration : IEntityTypeConfiguration<SurveyTarget>
{
    public void Configure(EntityTypeBuilder<SurveyTarget> builder)
    {
        builder.Property(e => e.SurveyTargetId).IsRequired();
        builder.Property(e => e.SurveyId).IsRequired();
    }
}