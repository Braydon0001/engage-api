namespace Engage.Persistence.Configurations;

public class SurveyFormReasonConfiguration : IEntityTypeConfiguration<SurveyFormReason>
{
    public void Configure(EntityTypeBuilder<SurveyFormReason> builder)
    {
        builder.Property(e => e.SurveyFormReasonId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Description).HasMaxLength(1000);
    }
}