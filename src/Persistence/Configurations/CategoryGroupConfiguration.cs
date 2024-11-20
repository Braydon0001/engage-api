namespace Engage.Persistence.Configurations;

public class CategoryGroupConfiguration : IEntityTypeConfiguration<CategoryGroup>
{
    public void Configure(EntityTypeBuilder<CategoryGroup> builder)
    {
        builder.Property(e => e.CategoryGroupId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}