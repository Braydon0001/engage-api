namespace Engage.Persistence.Configurations;

public class ClaimTypeReportTypeConfiguration : IEntityTypeConfiguration<ClaimTypeReportType>
{
    public void Configure(EntityTypeBuilder<ClaimTypeReportType> builder)
    {
        builder.HasKey(e => new { e.ClaimTypeId, e.ClaimReportTypeId })
              .IsClustered(false);

        builder.HasOne(e => e.ClaimType)
            .WithMany(e => e.ClaimTypeReportTypes)
            .HasForeignKey(e => e.ClaimTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(e => e.ClaimReportType)
            .WithMany(e => e.ClaimTypeReportTypes)
            .HasForeignKey(e => e.ClaimReportTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
