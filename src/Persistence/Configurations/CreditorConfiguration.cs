namespace Engage.Persistence.Configurations;

public class CreditorConfiguration : IEntityTypeConfiguration<Creditor>
{
    public void Configure(EntityTypeBuilder<Creditor> builder)
    {
        builder.Property(e => e.CreditorId).IsRequired();
        builder.Property(e => e.CreditorStatusId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(300);
        builder.Property(e => e.TradingName).IsRequired().HasMaxLength(300);
        builder.Property(e => e.IsVatRegistered);
        builder.Property(e => e.VatNumber).HasMaxLength(200);
        builder.Property(e => e.BankConfirmationDate).IsRequired();
        builder.Property(e => e.EvolutionCreditorNumber).HasMaxLength(200);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}