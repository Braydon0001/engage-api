namespace Engage.Persistence.Configurations;

public class IncidentSkuConfiguration : IEntityTypeConfiguration<IncidentSku>
{
    public void Configure(EntityTypeBuilder<IncidentSku> builder)
    {
        builder.HasIndex(e => new { e.IncidentId, e.DCProductId })
                 .IsUnique()
                 .IsClustered(false);

        builder.Property(e => e.Note)
               .HasMaxLength(300);
    }
}
