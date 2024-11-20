namespace Engage.Persistence.Configurations;

public class SurveyFormQuestionGroupConfiguration : IEntityTypeConfiguration<SurveyFormQuestionGroup>
{
    public void Configure(EntityTypeBuilder<SurveyFormQuestionGroup> builder)
    {
        builder.Property(e => e.SurveyFormQuestionGroupId).IsRequired();
        builder.Property(e => e.Name).IsRequired();
        builder.Property(e => e.DisplayOrder);
        builder.Property(e => e.IsRequired);
        builder.Property(e => e.Rules).HasColumnType("json");
        builder.Property(e => e.Files).HasColumnType("json");
        builder.Property(e => e.Metadata).HasColumnType("json");
        builder.Property(e => e.SurveyFormId).IsRequired();
        builder.Property(e => e.IsVirtualGroup);
        builder.Property(e => e.CategoryTargetValue);
        builder.Property(e => e.Links).HasColumnType("json");
    }
}