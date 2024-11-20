namespace Engage.Persistence.Configurations;

public class IncidentSkuStatusConfiguration : IEntityTypeConfiguration<IncidentSkuStatus>
{
    public void Configure(EntityTypeBuilder<IncidentSkuStatus> builder)
    {
        builder.Property(e => e.Name)
               .HasMaxLength(100)
               .IsRequired();
    }
}
