// auto-generated
namespace Engage.Persistence.Configurations;

public class SupplierSubContractDetailConfiguration : IEntityTypeConfiguration<SupplierSubContractDetail>
{
    public void Configure(EntityTypeBuilder<SupplierSubContractDetail> builder)
    {
        builder.Property(e => e.SupplierSubContractDetailId).IsRequired();
        builder.Property(e => e.SupplierSubContractTypeId).IsRequired();
        builder.Property(e => e.SupplierSubContractDetailTypeId);
        builder.Property(e => e.Detail).HasMaxLength(200);
        builder.Property(e => e.Note).HasMaxLength(200);
    }
}