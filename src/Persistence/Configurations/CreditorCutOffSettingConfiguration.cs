namespace Engage.Persistence.Configurations;

public class CreditorCutOffSettingConfiguration : IEntityTypeConfiguration<CreditorCutOffSetting>
{
    public void Configure(EntityTypeBuilder<CreditorCutOffSetting> builder)
    {
        builder.Property(e => e.CreditorCutOffSettingId).IsRequired();
        builder.Property(e => e.CreditorCutOff).IsRequired().HasMaxLength(30);
        builder.Property(e => e.PaymentCutOff).IsRequired().HasMaxLength(30);
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.EndDate).IsRequired();
    }
}
