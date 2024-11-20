namespace Engage.Persistence.Configurations;

public class EmailHistoryCCEmailConfiguration : IEntityTypeConfiguration<EmailHistoryCCEmail>
{
    public void Configure(EntityTypeBuilder<EmailHistoryCCEmail> builder)
    {
        builder.Property(e => e.Email)
               .HasMaxLength(100)
               .IsRequired();
    }
}
