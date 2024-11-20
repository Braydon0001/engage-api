namespace Engage.Persistence.Configurations;

public class StoreConceptAttributeOptionConfiguration : IEntityTypeConfiguration<StoreConceptAttributeOption>
{
    public void Configure(EntityTypeBuilder<StoreConceptAttributeOption> builder)
    {
        builder.Property(e => e.Option)
               .IsRequired()
               .HasMaxLength(100);
    }
}
