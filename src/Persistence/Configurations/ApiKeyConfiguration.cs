namespace Engage.Persistence.Configurations;

public class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
{
    public void Configure(EntityTypeBuilder<ApiKey> builder)
    {
        builder.Property(e => e.ApiKeyId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Value).IsRequired().HasMaxLength(200);
        builder.Property(e => e.AssignedTo).IsRequired().HasMaxLength(200);
    }
}