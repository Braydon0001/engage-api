namespace Engage.Persistence.Configurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(220);

            builder.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(1500);

            builder.Property(e => e.Subject)
                .HasMaxLength(220);

            builder.Property(e => e.Files)
                .HasColumnType("json");
        }
    }
}
