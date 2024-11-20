// auto-generated
namespace Engage.Persistence.Configurations;

public class SupplierContractConfiguration : IEntityTypeConfiguration<SupplierContract>
{
    public void Configure(EntityTypeBuilder<SupplierContract> builder)
    {
        builder.Property(e => e.SupplierContractId).IsRequired();
        builder.Property(e => e.SupplierId).IsRequired();
        builder.Property(e => e.SupplierContractTypeId).IsRequired();
        builder.Property(e => e.SupplierContractGroupId);
        builder.Property(e => e.SupplierContractSubGroupId);
        builder.Property(e => e.SupplierContactId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.EndDate).IsRequired();
        builder.Property(e => e.Vendor).HasMaxLength(100);
        builder.Property(e => e.Note).HasMaxLength(220);
        builder.Property(e => e.Reference1).HasMaxLength(100);
        builder.Property(e => e.Reference2).HasMaxLength(100);
        builder.Property(e => e.Files).HasColumnType("json");
        builder.Property(e => e.IsEngage);
        builder.Property(e => e.IsEncore);
        builder.Property(e => e.IsEngine);
        builder.Property(e => e.IsSpar);
        builder.Property(e => e.IsTops);
    }
}