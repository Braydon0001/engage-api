namespace Engage.Persistence.Configurations;

public class ProjectFileTypeConfiguration : IEntityTypeConfiguration<ProjectFileType>
{
    public void Configure(EntityTypeBuilder<ProjectFileType> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(300);
    }
}
