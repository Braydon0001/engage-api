namespace Engage.Persistence.Configurations;

public class SurveyFormQuestionGroupProductConfiguration : IEntityTypeConfiguration<SurveyFormQuestionGroupProduct>
{
    public void Configure(EntityTypeBuilder<SurveyFormQuestionGroupProduct> builder)
    {
        builder.Property(e => e.SurveyFormQuestionGroupProductId).IsRequired();
        builder.Property(e => e.SurveyFormQuestionGroupId).IsRequired();
        builder.Property(e => e.EngageVariantProductId).IsRequired();
    }
}