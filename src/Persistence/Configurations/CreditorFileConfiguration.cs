namespace Engage.Persistence.Configurations;

public class CreditorFileConfiguration : IEntityTypeConfiguration<CreditorFile>
{
    public void Configure(EntityTypeBuilder<CreditorFile> builder)
    {
        builder.Property(e => e.CreditorFileId).IsRequired();
        builder.Property(e => e.CreditorId).IsRequired();
        builder.Property(e => e.CreditorFileTypeId).IsRequired();
        builder.Property(e => e.Files).HasColumnType("json");
    }
}