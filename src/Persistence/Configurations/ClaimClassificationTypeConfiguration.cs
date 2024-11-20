namespace Engage.Persistence.Configurations;

public class ClaimClassificationTypeConfiguration : IEntityTypeConfiguration<ClaimClassificationType>
{
    public void Configure(EntityTypeBuilder<ClaimClassificationType> builder)
    {
        builder.HasKey(e => new { e.ClaimClassificationId, e.ClaimTypeId })
              .IsClustered(false);

        builder.HasOne(e => e.ClaimClassification)
            .WithMany(e => e.ClaimClassificationTypes)
            .HasForeignKey(e => e.ClaimClassificationId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(e => e.ClaimType)
            .WithMany(e => e.ClaimClassificationTypes)
            .HasForeignKey(e => e.ClaimTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
