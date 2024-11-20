namespace Engage.Persistence.Configurations;

public class EmployeeStoreCalendarSurveyFormSubmissionConfiguration : IEntityTypeConfiguration<EmployeeStoreCalendarSurveyFormSubmission>
{
    public void Configure(EntityTypeBuilder<EmployeeStoreCalendarSurveyFormSubmission> builder)
    {
        builder.Property(e => e.EmployeeStoreCalendarSurveyFormSubmissionId).IsRequired();
        builder.Property(e => e.EmployeeStoreCalendarId).IsRequired();
        builder.Property(e => e.SurveyFormSubmissionId).IsRequired();
    }
}