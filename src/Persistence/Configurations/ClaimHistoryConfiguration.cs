namespace Engage.Persistence.Configurations;

public class ClaimHistoryConfiguration : IEntityTypeConfiguration<ClaimHistory>
{
    public void Configure(EntityTypeBuilder<ClaimHistory> builder)
    {
        builder.Property(e => e.RejectedReason)
               .HasMaxLength(300);

        builder.Property(e => e.PendingReason)
               .HasMaxLength(300);
    }
}
