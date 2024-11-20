namespace Engage.Persistence.Configurations;

public class VoucherDetailConfiguration : IEntityTypeConfiguration<VoucherDetail>
{
    public void Configure(EntityTypeBuilder<VoucherDetail> builder)
    {
        builder.HasIndex(e => new { e.VoucherId, e.VoucherNumber })
               .IsUnique()
               .IsClustered(false);

        builder.Property(e => e.VoucherNumber)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(e => e.Note)
               .HasMaxLength(300);

        builder.Property(e => e.Files).HasColumnType("json");
    }
}
