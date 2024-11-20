namespace Engage.Persistence.Configurations;

public class FileUploadConfiguration : IEntityTypeConfiguration<FileUpload>
{
    public void Configure(EntityTypeBuilder<FileUpload> builder)
    {
        builder.Property(e => e.FileName)
               .IsRequired()
               .HasMaxLength(1000);
    }
}
