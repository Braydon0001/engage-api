namespace Engage.Persistence.Configurations
{
    public class ClaimNotificationUserConfiguration : IEntityTypeConfiguration<ClaimNotificationUser>
    {
        public void Configure(EntityTypeBuilder<ClaimNotificationUser> builder)
        {
            builder.HasIndex(e => new { e.ClaimStatusId, e.UserId, e.EngageRegionId })
                   .IsUnique()
                   .IsClustered(false);
        }
    }
}
