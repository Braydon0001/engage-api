namespace Engage.Persistence.Configurations;

public class SparSourceConfiguration : IEntityTypeConfiguration<SparSource>
{
    public void Configure(EntityTypeBuilder<SparSource> builder)
    {
        builder.Property(e => e.SparSourceId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(120);
    }
}