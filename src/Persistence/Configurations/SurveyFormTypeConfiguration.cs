namespace Engage.Persistence.Configurations;

public class SurveyFormTypeConfiguration : IEntityTypeConfiguration<SurveyFormType>
{
    public void Configure(EntityTypeBuilder<SurveyFormType> builder)
    {
        builder.Property(e => e.SurveyFormTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).HasMaxLength(1000);
        builder.Property(e => e.HideEmployeeTargeting);
        builder.Property(e => e.HideEngageSupplier);
        builder.Property(e => e.HideEndDate);
        builder.Property(e => e.HideRecurring);
        builder.Property(e => e.HideSurveyRequired);
        builder.Property(e => e.HideAddQuestionGroup);
        builder.Property(e => e.HideAddQuestion);
        builder.Property(e => e.HideReorderGroup);



    }
}