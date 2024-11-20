namespace Engage.Persistence.Configurations;

class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
{
    public void Configure(EntityTypeBuilder<EmailTemplate> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.FromEmailName)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.FromEmailAddress)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.EmailTypeId)
            .IsRequired();
    }
}
