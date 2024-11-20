namespace Engage.Persistence.Configurations.FileEntitities;

public class FileContainerConfiguration : IEntityTypeConfiguration<FileContainer>
{
    public void Configure(EntityTypeBuilder<FileContainer> builder)
    {
        builder.Property(e => e.Name)
              .HasMaxLength(120)
              .IsRequired();

        builder.Property(e => e.ContainerName)
              .HasMaxLength(1024)
              .IsRequired();

        builder.Property(e => e.FileNameStrategy)
              .HasMaxLength(30)
              .IsRequired();
    }
}
