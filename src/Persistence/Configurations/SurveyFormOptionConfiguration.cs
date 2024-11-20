namespace Engage.Persistence.Configurations;

public class SurveyFormOptionConfiguration : IEntityTypeConfiguration<SurveyFormOption>
{
    public void Configure(EntityTypeBuilder<SurveyFormOption> builder)
    {
        builder.Property(e => e.SurveyFormOptionId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).HasMaxLength(1000);
        builder.Property(e => e.CompleteSurvey);
    }
}