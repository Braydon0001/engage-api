namespace Engage.Persistence.Configurations;

public class SparSystemStatusConfiguration : IEntityTypeConfiguration<SparSystemStatus>
{
    public void Configure(EntityTypeBuilder<SparSystemStatus> builder)
    {
        builder.Property(e => e.SparSystemStatusId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(120);
    }
}