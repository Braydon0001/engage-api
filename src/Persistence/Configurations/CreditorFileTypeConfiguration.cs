namespace Engage.Persistence.Configurations;

public class CreditorFileTypeConfiguration : IEntityTypeConfiguration<CreditorFileType>
{
    public void Configure(EntityTypeBuilder<CreditorFileType> builder)
    {
        builder.Property(e => e.CreditorFileTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}