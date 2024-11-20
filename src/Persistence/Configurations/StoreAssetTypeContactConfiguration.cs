namespace Engage.Persistence.Configurations;

public class StoreAssetTypeContactConfiguration : IEntityTypeConfiguration<StoreAssetTypeContact>
{
    public void Configure(EntityTypeBuilder<StoreAssetTypeContact> builder)
    {
        builder.Property(e => e.StoreAssetTypeContactId).IsRequired();
        builder.Property(e => e.FirstName);
        builder.Property(e => e.LastName);
        builder.Property(e => e.EmailAddress).IsRequired();
        builder.Property(e => e.MobilePhone);
    }
}