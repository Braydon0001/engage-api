namespace Engage.Persistence.Configurations;

public class ClaimFloatConfiguration : IEntityTypeConfiguration<ClaimFloat>
{
    public void Configure(EntityTypeBuilder<ClaimFloat> builder)
    {
        //builder.HasIndex(e => new { e.SupplierId, e.EngageRegionId })
        //       .IsUnique()
        //       .IsClustered(false);

        builder.Property(e => e.Title)
               .HasMaxLength(300);

        builder.Property(e => e.Reference)
               .HasMaxLength(220);

    }
}
