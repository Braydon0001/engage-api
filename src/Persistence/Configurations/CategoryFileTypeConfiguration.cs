namespace Engage.Persistence.Configurations;

public class CategoryFileTypeConfiguration : IEntityTypeConfiguration<CategoryFileType>
{
    public void Configure(EntityTypeBuilder<CategoryFileType> builder)
    {
        builder.Property(e => e.CategoryFileTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}