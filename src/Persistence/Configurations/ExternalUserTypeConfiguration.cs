namespace Engage.Persistence.Configurations;

public class ExternalUserTypeConfiguration : IEntityTypeConfiguration<ExternalUserType>
{
    public void Configure(EntityTypeBuilder<ExternalUserType> builder)
    {
        builder.Property(e => e.ExternalUserTypeId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}