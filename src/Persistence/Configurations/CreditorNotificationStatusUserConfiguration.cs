namespace Engage.Persistence.Configurations;

public class CreditorNotifcationStatusUserConfiguration : IEntityTypeConfiguration<CreditorNotificationStatusUser>
{
    public void Configure(EntityTypeBuilder<CreditorNotificationStatusUser> builder)
    {
        builder.Property(e => e.CreditorNotificationStatusUserId).IsRequired();
        builder.Property(e => e.CreditorStatusId).IsRequired();
        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.EngageRegionId);
    }
}