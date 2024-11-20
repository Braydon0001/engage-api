namespace Engage.Persistence.Configurations;

public class CategoryTargetTypeConfiguration : IEntityTypeConfiguration<CategoryTargetType>
{
    public void Configure(EntityTypeBuilder<CategoryTargetType> builder)
    {
        builder.Property(e => e.CategoryTargetTypeId).IsRequired();
        builder.Property(e => e.Name);
    }
}