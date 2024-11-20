namespace Engage.Persistence.Configurations;

public class AnalysisPillarSubGroupConfiguration : IEntityTypeConfiguration<AnalysisPillarSubGroup>
{
    public void Configure(EntityTypeBuilder<AnalysisPillarSubGroup> builder)
    {
        builder.Property(e => e.AnalysisPillarSubGroupId).IsRequired();
        builder.Property(e => e.AnalysisPillarGroupId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(120);
    }
}