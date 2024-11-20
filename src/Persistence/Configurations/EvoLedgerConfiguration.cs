namespace Engage.Persistence.Configurations;

public class EvoLedgerConfiguration : IEntityTypeConfiguration<EvoLedger>
{
    public void Configure(EntityTypeBuilder<EvoLedger> builder)
    {
        builder.Property(e => e.EvoLedgerId).IsRequired();
        builder.Property(e => e.LedgerCode).IsRequired().HasMaxLength(20);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(200);
        builder.Property(e => e.AnalysisPillarSubGroupId).IsRequired();
        builder.Property(e => e.IsActive).IsRequired();
    }
}