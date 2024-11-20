// auto-generated
namespace Engage.Persistence.Configurations;

public class SurveyStoreFormatTargetConfiguration : IEntityTypeConfiguration<SurveyStoreFormatTarget>
{
    public void Configure(EntityTypeBuilder<SurveyStoreFormatTarget> builder)
    {
        builder.Property(e => e.StoreFormatId).IsRequired();

        // Multi-column indexes 

        builder.HasIndex(e => new { e.SurveyId, e.StoreFormatId }).IsUnique();
    }
}