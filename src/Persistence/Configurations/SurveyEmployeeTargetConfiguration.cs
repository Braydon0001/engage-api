// auto-generated
namespace Engage.Persistence.Configurations;

public class SurveyEmployeeTargetConfiguration : IEntityTypeConfiguration<SurveyEmployeeTarget>
{
    public void Configure(EntityTypeBuilder<SurveyEmployeeTarget> builder)
    {
        builder.Property(e => e.EmployeeId).IsRequired();

        // Multi-column indexes 

        builder.HasIndex(e => new { e.SurveyId, e.EmployeeId }).IsUnique();
    }
}