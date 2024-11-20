namespace Engage.Persistence.Configurations;

public class SurveyFormSubmissionConfiguration : IEntityTypeConfiguration<SurveyFormSubmission>
{
    public void Configure(EntityTypeBuilder<SurveyFormSubmission> builder)
    {
        builder.Property(e => e.SurveyFormSubmissionId).IsRequired();
        builder.Property(e => e.EmployeeId);
        builder.Property(e => e.UserId);
        builder.Property(e => e.SurveyFormId).IsRequired();
        builder.Property(e => e.StoreId);
        builder.Property(e => e.SubmissionUuid).IsRequired();
        builder.Property(e => e.StartedDate).IsRequired();
        builder.Property(e => e.IsComplete);
        builder.Property(e => e.CompletedDate);
        builder.Property(e => e.IsAbandoned);
        builder.Property(e => e.AbandonedDate);
        builder.Property(e => e.Note);
    }
}