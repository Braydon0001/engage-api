namespace Engage.Persistence.Configurations;

public class EmailHistoryConfiguration : IEntityTypeConfiguration<EmailHistory>
{
    public void Configure(EntityTypeBuilder<EmailHistory> builder)
    {
        builder.Property(e => e.ToEmail)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(e => e.Subject)
               .HasMaxLength(300);
    }
}
