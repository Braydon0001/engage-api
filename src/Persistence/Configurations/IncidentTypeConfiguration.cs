namespace Engage.Persistence.Configurations;

public class IncidentTypeConfiguration : IEntityTypeConfiguration<IncidentType>
{
    public void Configure(EntityTypeBuilder<IncidentType> builder)
    {
        builder.Property(e => e.Name)
               .HasMaxLength(100)
               .IsRequired();
    }
}
