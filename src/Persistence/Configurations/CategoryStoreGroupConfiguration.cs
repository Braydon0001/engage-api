namespace Engage.Persistence.Configurations;

public class CategoryStoreGroupConfiguration : IEntityTypeConfiguration<CategoryStoreGroup>
{
    public void Configure(EntityTypeBuilder<CategoryStoreGroup> builder)
    {
        builder.Property(e => e.CategoryStoreGroupId).IsRequired();
        builder.Property(e => e.CategoryGroupId);
        builder.Property(e => e.StoreId);
    }
}