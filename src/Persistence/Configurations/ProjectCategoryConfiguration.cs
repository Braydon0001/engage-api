namespace Engage.Persistence.Configurations;

public class ProjectCategoryConfiguration : IEntityTypeConfiguration<ProjectCategory>
{
    public void Configure(EntityTypeBuilder<ProjectCategory> builder)
    {
        builder.Property(e => e.ProjectCategoryId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}