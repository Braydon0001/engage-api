namespace Engage.Persistence.Configurations;

public class ProjectCategorySupplierConfiguration : IEntityTypeConfiguration<ProjectCategorySupplier>
{
    public void Configure(EntityTypeBuilder<ProjectCategorySupplier> builder)
    {
        builder.Property(e => e.ProjectCategorySupplierId).IsRequired();
        builder.Property(e => e.ProjectCategoryId).IsRequired();
        builder.Property(e => e.SupplierId).IsRequired();
    }
}