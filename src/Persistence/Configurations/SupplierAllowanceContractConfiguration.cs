namespace Engage.Persistence.Configurations;

public class SupplierAllowanceContractConfiguration : IEntityTypeConfiguration<SupplierAllowanceContract>
{
    public void Configure(EntityTypeBuilder<SupplierAllowanceContract> builder)
    {
        builder.Property(e => e.SupplierId).IsRequired();
        builder.Property(e => e.SupplierSalesLeadId).IsRequired();
        builder.Property(e => e.Name).HasMaxLength(100);
        builder.Property(e => e.Vendor).HasMaxLength(100);
        builder.Property(e => e.NCircularReference).HasMaxLength(100);
        builder.Property(e => e.EncoreReference).HasMaxLength(100);
        builder.Property(e => e.StartDate);
        builder.Property(e => e.EndDate);
        builder.Property(e => e.Comment).HasMaxLength(220);
        builder.Property(e => e.Note).HasMaxLength(220);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}
