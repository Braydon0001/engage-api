namespace Engage.Persistence.Configurations;

public class EngageVariantProductConfiguration : IEntityTypeConfiguration<EngageVariantProduct>
{
    public void Configure(EntityTypeBuilder<EngageVariantProduct> builder)
    {
        builder.Property(e => e.Name).IsRequired().HasMaxLength(220);
        builder.Property(e => e.Code).IsRequired().HasMaxLength(30);
        builder.Property(e => e.Size).IsRequired();
        builder.Property(e => e.PackSize).IsRequired();
        builder.Property(e => e.EANNumber).HasMaxLength(20);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}
