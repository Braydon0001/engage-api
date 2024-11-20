namespace Engage.Persistence.Configurations;

public class CreditorStatusHistoryConfiguration : IEntityTypeConfiguration<CreditorStatusHistory>
{
    public void Configure(EntityTypeBuilder<CreditorStatusHistory> builder)
    {
        builder.Property(e => e.CreditorStatusHistoryId).IsRequired();
        builder.Property(e => e.CreditorId).IsRequired();
        builder.Property(e => e.CreditorStatusId).IsRequired();
        builder.Property(e => e.Reason).HasMaxLength(300);
    }
}