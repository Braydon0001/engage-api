namespace Engage.Persistence.Configurations;

public class SurveyFormQuestionOptionConfiguration : IEntityTypeConfiguration<SurveyFormQuestionOption>
{
    public void Configure(EntityTypeBuilder<SurveyFormQuestionOption> builder)
    {
        builder.Property(e => e.SurveyFormQuestionOptionId).IsRequired();
        builder.Property(e => e.SurveyFormQuestionId).IsRequired();
        builder.Property(e => e.SurveyFormOptionId).IsRequired();
    }
}