namespace Engage.Persistence.Configurations;

public class StoreAssetFileTypeConfiguration : IEntityTypeConfiguration<StoreAssetFileType>
{
    public void Configure(EntityTypeBuilder<StoreAssetFileType> builder)
    {
        builder.Property(e => e.StoreAssetFileTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(120);
        builder.Property(e => e.Description).HasMaxLength(300);
    }
}
