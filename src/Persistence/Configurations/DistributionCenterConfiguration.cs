namespace Engage.Persistence.Configurations;

public class DistributionCenterConfiguration : IEntityTypeConfiguration<DistributionCenter>
{
    public void Configure(EntityTypeBuilder<DistributionCenter> builder)
    {
        builder.Property(e => e.Code)
            .HasMaxLength(20);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(120);
    }
}
