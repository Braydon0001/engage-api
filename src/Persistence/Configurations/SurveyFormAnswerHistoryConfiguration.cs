namespace Engage.Persistence.Configurations;

public class SurveyFormAnswerHistoryConfiguration : IEntityTypeConfiguration<SurveyFormAnswerHistory>
{
    public void Configure(EntityTypeBuilder<SurveyFormAnswerHistory> builder)
    {
        builder.Property(e => e.SurveyFormAnswerHistoryId).IsRequired();
        builder.Property(e => e.AnswerText).IsRequired();
        builder.Property(e => e.Files).HasColumnType("json");
        builder.Property(e => e.SurveyFormAnswerId).IsRequired();
        builder.Property(e => e.SurveyFormReasonId);
    }
}