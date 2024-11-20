namespace Engage.Persistence.Configurations;

public class ProjectSubCategoryConfiguration : IEntityTypeConfiguration<ProjectSubCategory>
{
    public void Configure(EntityTypeBuilder<ProjectSubCategory> builder)
    {
        builder.Property(e => e.ProjectSubCategoryId).IsRequired();
        builder.Property(e => e.ProjectCategoryId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}