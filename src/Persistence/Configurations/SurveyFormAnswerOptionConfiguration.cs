namespace Engage.Persistence.Configurations;

public class SurveyFormAnswerOptionConfiguration : IEntityTypeConfiguration<SurveyFormAnswerOption>
{
    public void Configure(EntityTypeBuilder<SurveyFormAnswerOption> builder)
    {
        builder.Property(e => e.SurveyFormAnswerOptionId).IsRequired();
        builder.Property(e => e.SurveyFormAnswerId).IsRequired();
        builder.Property(e => e.SurveyFormOptionId).IsRequired();
    }
}