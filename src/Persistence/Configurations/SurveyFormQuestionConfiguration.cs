namespace Engage.Persistence.Configurations;

public class SurveyFormQuestionConfiguration : IEntityTypeConfiguration<SurveyFormQuestion>
{
    public void Configure(EntityTypeBuilder<SurveyFormQuestion> builder)
    {
        builder.Property(e => e.SurveyFormQuestionId).IsRequired();
        builder.Property(e => e.QuestionText).IsRequired();
        builder.Property(e => e.DisplayOrder);
        builder.Property(e => e.IsRequired);
        builder.Property(e => e.Notes);
        builder.Property(e => e.Rules).HasColumnType("json");
        builder.Property(e => e.Files).HasColumnType("json");
        builder.Property(e => e.Metadata).HasColumnType("json");
        builder.Property(e => e.SurveyFormQuestionGroupId).IsRequired();
        builder.Property(e => e.SurveyFormQuestionTypeId).IsRequired();
        builder.Property(e => e.IsReasonRequired);
        builder.Property(e => e.MinDateTime);
        builder.Property(e => e.MaxDateTime);
        builder.Property(e => e.Links).HasColumnType("json");
    }
}