namespace Engage.Persistence.Configurations
{
    public class WebEventConfiguration : IEntityTypeConfiguration<WebEvent>
    {
        public void Configure(EntityTypeBuilder<WebEvent> builder)
        {
            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(e => e.StartDate)
                .IsRequired();
        }
    }
}
