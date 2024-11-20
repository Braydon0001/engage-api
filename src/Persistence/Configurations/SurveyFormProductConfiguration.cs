namespace Engage.Persistence.Configurations;

public class SurveyFormProductConfiguration : IEntityTypeConfiguration<SurveyFormProduct>
{
    public void Configure(EntityTypeBuilder<SurveyFormProduct> builder)
    {
        builder.Property(e => e.SurveyFormProductId).IsRequired();
        builder.Property(e => e.SurveyFormId).IsRequired();
        builder.Property(e => e.EngageMasterProductId).IsRequired();
    }
}