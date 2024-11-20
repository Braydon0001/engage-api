namespace Engage.Persistence.Configurations;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(120);

        builder.Property(e => e.Code)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(e => e.VATNumber)
            .HasMaxLength(15);

        builder.Property(e => e.ClaimReportTitle)
            .HasMaxLength(200);

        builder.Property(e => e.ClaimReportAccountNumber)
            .HasMaxLength(200);

        builder.Property(e => e.ShortName)
            .HasMaxLength(30);

        builder.HasOne(x => x.Stakeholder)
            .WithOne(s => s.Supplier)
            .HasForeignKey<Supplier>(k => k.StakeholderId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.Property(e => e.Files).HasColumnType("json");
        builder.Property(e => e.Settings).HasColumnType("json");
        builder.Property(e => e.StringSettings).HasColumnType("json");
        builder.Property(e => e.NumberSettings).HasColumnType("json");
        builder.Property(e => e.BooleanSettings).HasColumnType("json");
        builder.Property(e => e.JsonTheme).HasColumnType("json");
    }
}
