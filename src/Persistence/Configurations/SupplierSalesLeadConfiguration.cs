// auto-generated
namespace Engage.Persistence.Configurations;

public class SupplierSalesLeadConfiguration : IEntityTypeConfiguration<SupplierSalesLead>
{
    public void Configure(EntityTypeBuilder<SupplierSalesLead> builder)
    {
        builder.Property(e => e.SupplierSalesLeadId).IsRequired();
        builder.Property(e => e.FirstName);
        builder.Property(e => e.LastName);
        builder.Property(e => e.KnownAs).HasMaxLength(100);
        builder.Property(e => e.EmailAddress).HasMaxLength(100);
        builder.Property(e => e.ContactNumber).HasMaxLength(100);
    }
}