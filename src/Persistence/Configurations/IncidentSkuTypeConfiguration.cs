namespace Engage.Persistence.Configurations;

public class IncidentSkuTypeConfiguration : IEntityTypeConfiguration<IncidentSkuType>
{
    public void Configure(EntityTypeBuilder<IncidentSkuType> builder)
    {
        builder.Property(e => e.Name)
               .HasMaxLength(100)
               .IsRequired();
    }
}
