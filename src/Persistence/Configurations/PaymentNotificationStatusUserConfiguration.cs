namespace Engage.Persistence.Configurations;

public class PaymentNotifcationStatusUserConfiguration : IEntityTypeConfiguration<PaymentNotificationStatusUser>
{
    public void Configure(EntityTypeBuilder<PaymentNotificationStatusUser> builder)
    {
        builder.Property(e => e.PaymentNotificationStatusUserId).IsRequired();
        builder.Property(e => e.PaymentStatusId).IsRequired();
        builder.Property(e => e.UserId).IsRequired();
        builder.Property(e => e.EngageRegionId).IsRequired();
    }
}