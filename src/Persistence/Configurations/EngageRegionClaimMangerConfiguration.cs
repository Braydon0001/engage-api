namespace Engage.Persistence.Configurations
{
    public class EngageRegionClaimMangerConfiguration : IEntityTypeConfiguration<EngageRegionClaimManager>
    {
        public void Configure(EntityTypeBuilder<EngageRegionClaimManager> builder)
        {
            builder.HasKey(e => new { e.EngageRegionId, e.UserId })
                   .IsClustered(false);

            builder.HasOne(x => x.EngageRegion)
                .WithMany(c => c.EngageRegionClaimManagers)
                .HasForeignKey(x => x.EngageRegionId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.User)
                .WithMany(c => c.EngageRegionClaimManagers)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
