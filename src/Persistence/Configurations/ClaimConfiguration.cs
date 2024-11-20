namespace Engage.Persistence.Configurations;

public class ClaimConfiguration : IEntityTypeConfiguration<Claim>
{
    public void Configure(EntityTypeBuilder<Claim> builder)
    {
        builder.HasIndex(e => e.ClaimNumber)
               .IsClustered(false);

        builder.Property(e => e.ClaimNumber)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(e => e.ClaimReference)
               .HasMaxLength(220);

        builder.Property(e => e.Comment)
               .HasMaxLength(300);

        builder.Property(e => e.RejectedReason)
               .HasMaxLength(300);

        builder.Property(e => e.PendingReason)
               .HasMaxLength(300);
    }
}
