namespace Engage.Persistence.Configurations;

public class ProjectSupplierConfiguration : IEntityTypeConfiguration<ProjectSupplier>
{
    public void Configure(EntityTypeBuilder<ProjectSupplier> builder)
    {
        builder.Property(e => e.ProjectSupplierId).IsRequired();
        builder.Property(e => e.ProjectId).IsRequired();
        builder.Property(e => e.SupplierId).IsRequired();
    }
}