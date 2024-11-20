namespace Engage.Persistence.Configurations;

public class SurveyFormQuestionTypeConfiguration : IEntityTypeConfiguration<SurveyFormQuestionType>
{
    public void Configure(EntityTypeBuilder<SurveyFormQuestionType> builder)
    {
        builder.Property(e => e.SurveyFormQuestionTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).HasMaxLength(1000);
    }
}