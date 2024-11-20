namespace Engage.Persistence.Configurations;

public class SurveyFormConfiguration : IEntityTypeConfiguration<SurveyForm>
{
    public void Configure(EntityTypeBuilder<SurveyForm> builder)
    {
        builder.Property(e => e.SurveyFormId).IsRequired();
        builder.Property(e => e.Title).IsRequired();
        builder.Property(e => e.Description);
        builder.Property(e => e.Note);
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.EndDate);
        builder.Property(e => e.IsRequired);
        builder.Property(e => e.IsRecurring);
        builder.Property(e => e.IsDisabled);
        builder.Property(e => e.Files).HasColumnType("json");
        builder.Property(e => e.Rules).HasColumnType("json");
        builder.Property(e => e.SurveyFormTypeId).IsRequired();
        builder.Property(e => e.EngageSubgroupId);
        builder.Property(e => e.SupplierId);
        builder.Property(e => e.EngageBrandId);
        builder.Property(e => e.IsStoreRecurring);
        builder.Property(e => e.IsEmployeeSurvey);
        builder.Property(e => e.IgnoreSubgroup);
        builder.Property(e => e.Links).HasColumnType("json");
    }
}