namespace Engage.Persistence.Configurations;

public class SurveyFormQuestionProductConfiguration : IEntityTypeConfiguration<SurveyFormQuestionProduct>
{
    public void Configure(EntityTypeBuilder<SurveyFormQuestionProduct> builder)
    {
        builder.Property(e => e.SurveyFormQuestionProductId).IsRequired();
        builder.Property(e => e.SurveyFormQuestionId).IsRequired();
        builder.Property(e => e.EngageVariantProductId).IsRequired();
    }
}