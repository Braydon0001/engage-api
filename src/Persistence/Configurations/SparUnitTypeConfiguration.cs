namespace Engage.Persistence.Configurations;

public class SparUnitTypeConfiguration : IEntityTypeConfiguration<SparUnitType>
{
    public void Configure(EntityTypeBuilder<SparUnitType> builder)
    {
        builder.Property(e => e.SparUnitTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(120);
    }
}