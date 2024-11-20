// auto-generated
namespace Engage.Persistence.Configurations;

public class SupplierContractDetailConfiguration : IEntityTypeConfiguration<SupplierContractDetail>
{
    public void Configure(EntityTypeBuilder<SupplierContractDetail> builder)
    {
        builder.Property(e => e.SupplierContractDetailId).IsRequired();
        builder.Property(e => e.SupplierContractId).IsRequired();
        builder.Property(e => e.SupplierContractDetailTypeId).IsRequired();
        builder.Property(e => e.EngageRegionId);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Amount).IsRequired();
        builder.Property(e => e.RangeStartAmount);
        builder.Property(e => e.RangeEndAmount);
        builder.Property(e => e.GlCode).IsRequired().HasMaxLength(100);
        builder.Property(e => e.GlSubCode).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Note).IsRequired().HasMaxLength(220);
        builder.Property(e => e.Reference1).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}