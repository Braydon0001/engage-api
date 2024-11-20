namespace Engage.Persistence.Configurations;

public class CategorySubGroupConfiguration : IEntityTypeConfiguration<CategorySubGroup>
{
    public void Configure(EntityTypeBuilder<CategorySubGroup> builder)
    {
        builder.Property(e => e.CategorySubGroupId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}