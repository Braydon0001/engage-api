namespace Engage.Persistence.Configurations;

public class SurveyEngageRegionConfiguration : IEntityTypeConfiguration<SurveyEngageRegion>
{
    public void Configure(EntityTypeBuilder<SurveyEngageRegion> builder)
    {
        builder.HasKey(e => new { e.SurveyId, e.EngageRegionId })
            .IsClustered(false);

        builder.HasOne(x => x.Survey)
            .WithMany(s => s.SurveyEngageRegions)
            .HasForeignKey(x => x.SurveyId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.EngageRegion)
            .WithMany(s => s.SurveyEngageRegions)
            .HasForeignKey(x => x.EngageRegionId)
            .OnDelete(DeleteBehavior.ClientSetNull);

    }
}
