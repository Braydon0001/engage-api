namespace Engage.Persistence.Configurations;

public class CreditorStatusConfiguration : IEntityTypeConfiguration<CreditorStatus>
{
    public void Configure(EntityTypeBuilder<CreditorStatus> builder)
    {
        builder.Property(e => e.CreditorStatusId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
    }
}