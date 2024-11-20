namespace Engage.Persistence.Configurations;

public class EngageMasterProductConfiguration : IEntityTypeConfiguration<EngageMasterProduct>
{
    public void Configure(EntityTypeBuilder<EngageMasterProduct> builder)
    {
        builder.Property(e => e.Name).IsRequired().HasMaxLength(220);
        builder.Property(e => e.Code).IsRequired().HasMaxLength(30);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}
