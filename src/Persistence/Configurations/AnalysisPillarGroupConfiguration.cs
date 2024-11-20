namespace Engage.Persistence.Configurations;

public class AnalysisPillarGroupConfiguration : IEntityTypeConfiguration<AnalysisPillarGroup>
{
    public void Configure(EntityTypeBuilder<AnalysisPillarGroup> builder)
    {
        builder.Property(e => e.AnalysisPillarGroupId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(120);
    }
}