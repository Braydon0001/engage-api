namespace Engage.Persistence.Configurations;

public class SurveyFormAnswerConfiguration : IEntityTypeConfiguration<SurveyFormAnswer>
{
    public void Configure(EntityTypeBuilder<SurveyFormAnswer> builder)
    {
        builder.Property(e => e.SurveyFormAnswerId).IsRequired();
        builder.Property(e => e.AnswerText).IsRequired();
        builder.Property(e => e.Files).HasColumnType("json");
        builder.Property(e => e.Metadata).HasColumnType("json");
        builder.Property(e => e.SurveyFormSubmissionId).IsRequired();
        builder.Property(e => e.SurveyFormQuestionId).IsRequired();
        builder.Property(e => e.SurveyFormReasonId);
        builder.Property(e => e.AnswerUuid).IsRequired();
        builder.Property(e => e.AnswerDate);
    }
}