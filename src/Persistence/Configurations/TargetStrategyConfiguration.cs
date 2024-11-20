namespace Engage.Persistence.Configurations;

public class TargetStrategyConfiguration : IEntityTypeConfiguration<TargetStrategy>
{
    public void Configure(EntityTypeBuilder<TargetStrategy> builder)
    {
        builder.Property(e => e.TargetStrategyId);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(30);
    }
}
