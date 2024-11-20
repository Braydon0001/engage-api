namespace Engage.Persistence.Configurations;

public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
{
    public void Configure(EntityTypeBuilder<Incident> builder)
    {
        builder.HasIndex(e => e.IncidentNumber)
               .IsClustered(false);

        builder.Property(e => e.IncidentNumber)
               .IsRequired()
               .HasMaxLength(30);

        builder.Property(e => e.Note)
               .HasMaxLength(300);
    }
}
