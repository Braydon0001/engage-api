namespace Engage.Persistence.Configurations;

public class SurveyFormAnswerOptionHistoryConfiguration : IEntityTypeConfiguration<SurveyFormAnswerOptionHistory>
{
    public void Configure(EntityTypeBuilder<SurveyFormAnswerOptionHistory> builder)
    {
        builder.Property(e => e.SurveyFormAnswerOptionHistoryId).IsRequired();
        builder.Property(e => e.SurveyFormAnswerHistoryId).IsRequired();
        builder.Property(e => e.SurveyFormOptionId);
    }
}