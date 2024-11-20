namespace Engage.Persistence.Configurations.FileEntitities;

public class FileTypeConfiguration : IEntityTypeConfiguration<FileType>
{
    public void Configure(EntityTypeBuilder<FileType> builder)
    {
        builder.Property(e => e.Name)
               .IsRequired()
               .HasMaxLength(30);

        builder.Property(e => e.Icon)
               .HasMaxLength(120);
    }
}