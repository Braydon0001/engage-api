namespace Engage.Persistence.Configurations;

public class ClaimFloatClaimConfiguration : IEntityTypeConfiguration<ClaimFloatClaim>
{
    public void Configure(EntityTypeBuilder<ClaimFloatClaim> builder)
    {
        builder.HasKey(e => new { e.ClaimFloatId, e.ClaimId })
              .IsClustered(false);

        builder.HasOne(e => e.ClaimFloat)
            .WithMany(e => e.ClaimFloatClaims)
            .HasForeignKey(e => e.ClaimFloatId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(e => e.Claim)
            .WithMany(e => e.ClaimFloatClaims)
            .HasForeignKey(e => e.ClaimId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
