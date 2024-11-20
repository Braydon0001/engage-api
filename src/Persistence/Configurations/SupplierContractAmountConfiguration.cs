// auto-generated
namespace Engage.Persistence.Configurations;

public class SupplierContractAmountConfiguration : IEntityTypeConfiguration<SupplierContractAmount>
{
    public void Configure(EntityTypeBuilder<SupplierContractAmount> builder)
    {
        builder.Property(e => e.SupplierContractAmountId).IsRequired();
        builder.Property(e => e.SupplierSubContractDetailId).IsRequired();
        builder.Property(e => e.SupplierContractAmountTypeId).IsRequired();
        builder.Property(e => e.SupplierContractSplitId);
        builder.Property(e => e.Amount).IsRequired();
        builder.Property(e => e.StartRangeAmount);
        builder.Property(e => e.EndRangeAmount);
        builder.Property(e => e.IsAmountPercent);
        builder.Property(e => e.IsRangeAmountPercent);
        builder.Property(e => e.Note).HasColumnType("json");
    }
}