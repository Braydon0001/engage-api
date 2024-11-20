namespace Engage.Persistence.Configurations;

public class ProjectDcProductConfiguration : IEntityTypeConfiguration<ProjectDcProduct>
{
    public void Configure(EntityTypeBuilder<ProjectDcProduct> builder)
    {
        builder.Property(e => e.ProjectDcProductId).IsRequired();
        builder.Property(e => e.ProjectId).IsRequired();
        builder.Property(e => e.DcProductId).IsRequired();
    }
}