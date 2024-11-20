namespace Engage.Persistence.Configurations;

public class SparAnalysisGroupConfiguration : IEntityTypeConfiguration<SparAnalysisGroup>
{
    public void Configure(EntityTypeBuilder<SparAnalysisGroup> builder)
    {
        builder.Property(e => e.SparAnalysisGroupId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(120);
    }
}