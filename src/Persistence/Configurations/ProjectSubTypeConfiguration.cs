namespace Engage.Persistence.Configurations;

public class ProjectSubTypeConfiguration : IEntityTypeConfiguration<ProjectSubType>
{
    public void Configure(EntityTypeBuilder<ProjectSubType> builder)
    {
        builder.Property(e => e.ProjectSubTypeId).IsRequired();
        builder.Property(e => e.ProjectTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}