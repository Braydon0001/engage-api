namespace Engage.Persistence.Configurations;

public class StoreAssetConfiguration : IEntityTypeConfiguration<StoreAsset>
{
    public void Configure(EntityTypeBuilder<StoreAsset> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(e => e.Description)
            .HasMaxLength(100);

        builder.Property(e => e.SerialNumber)
            .HasMaxLength(100);

        builder.Property(e => e.Note)
            .HasMaxLength(200);

        builder.Property(e => e.Files)
            .HasColumnType("json");

        builder.Property(e => e.EmailAddress)
            .HasMaxLength(200);
    }
}
