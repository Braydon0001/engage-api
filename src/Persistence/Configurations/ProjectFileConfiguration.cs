namespace Engage.Persistence.Configurations;

public class ProjectFileConfiguration : IEntityTypeConfiguration<ProjectFile>
{
    public void Configure(EntityTypeBuilder<ProjectFile> builder)
    {
        builder.Property(e => e.Files).HasColumnType("json");
    }
}
