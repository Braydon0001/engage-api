namespace Engage.Persistence.Configurations;

public class UserEngageSubGroupConfiguration : IEntityTypeConfiguration<UserEngageSubGroup>
{
    public void Configure(EntityTypeBuilder<UserEngageSubGroup> builder)
    {
        builder.Property(e => e.UserEngageSubGroupId).IsRequired();
        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.EngageSubGroupId).IsRequired();
    }
}