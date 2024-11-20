namespace Engage.Persistence.Configurations;

public class StoreFilterUploadConfiguration : IEntityTypeConfiguration<StoreFilterUpload>
{
    public void Configure(EntityTypeBuilder<StoreFilterUpload> builder)
    {
        builder.Property(e => e.Filter)
               .HasMaxLength(120);

        builder.Property(e => e.StoreName)
               .HasMaxLength(120);
    }
}
