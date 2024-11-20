namespace Engage.Persistence.Configurations;

public class SurveyFormQuestionReasonConfiguration : IEntityTypeConfiguration<SurveyFormQuestionReason>
{
    public void Configure(EntityTypeBuilder<SurveyFormQuestionReason> builder)
    {
        builder.Property(e => e.SurveyFormQuestionReasonId).IsRequired();
        builder.Property(e => e.SurveyFormQuestionId).IsRequired();
        builder.Property(e => e.SurveyFormReasonId).IsRequired();
    }
}