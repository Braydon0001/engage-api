namespace Engage.Persistence.Configurations;

public class IncidentStatusConfiguration : IEntityTypeConfiguration<IncidentStatus>
{
    public void Configure(EntityTypeBuilder<IncidentStatus> builder)
    {
        builder.Property(e => e.Name)
               .HasMaxLength(100)
               .IsRequired();
    }
}
