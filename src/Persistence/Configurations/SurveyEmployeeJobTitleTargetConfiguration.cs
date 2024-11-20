// auto-generated
namespace Engage.Persistence.Configurations;

public class SurveyEmployeeJobTitleTargetConfiguration : IEntityTypeConfiguration<SurveyEmployeeJobTitleTarget>
{
    public void Configure(EntityTypeBuilder<SurveyEmployeeJobTitleTarget> builder)
    {
        builder.Property(e => e.EmployeeJobTitleId).IsRequired();

        // Multi-column indexes 

        builder.HasIndex(e => new { e.SurveyId, e.EmployeeJobTitleId }).IsUnique();
    }
}