// auto-generated
namespace Engage.Persistence.Configurations;

public class SupplierSubContractConfiguration : IEntityTypeConfiguration<SupplierSubContract>
{
    public void Configure(EntityTypeBuilder<SupplierSubContract> builder)
    {
        builder.Property(e => e.SupplierSubContractId).IsRequired();
        builder.Property(e => e.SupplierContractId).IsRequired();
        builder.Property(e => e.SupplierSubContractTypeId).IsRequired();
        builder.Property(e => e.Name).HasMaxLength(100);
        builder.Property(e => e.Reference1).HasMaxLength(100);
        builder.Property(e => e.GlMainCode).HasMaxLength(100);
        builder.Property(e => e.GlSubCode).HasMaxLength(100);
        builder.Property(e => e.Note).HasMaxLength(220);
    }
}