namespace Engage.Persistence.Configurations;

public class CategoryTargetStoreConfiguration : IEntityTypeConfiguration<CategoryTargetStore>
{
    public void Configure(EntityTypeBuilder<CategoryTargetStore> builder)
    {
        builder.Property(e => e.CategoryTargetStoreId);
        builder.Property(e => e.CategoryTargetId).IsRequired();
        builder.Property(e => e.StoreId).IsRequired();
    }
}