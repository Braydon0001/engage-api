namespace Engage.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.Property(e => e.DCAccountNo).HasMaxLength(120);
        builder.Property(e => e.OrderNo).HasMaxLength(30);
        builder.Property(e => e.OrderReference).HasMaxLength(220);
        builder.Property(e => e.Comment).HasMaxLength(300);
        builder.Property(e => e.Note).HasMaxLength(300);
        builder.Property(e => e.VATNumber).HasMaxLength(100);
        builder.Property(e => e.AccountNumber).HasMaxLength(100);
        builder.Property(e => e.Email).HasMaxLength(200);
        builder.Property(e => e.Contact).HasMaxLength(200);
        builder.Property(e => e.Address).HasMaxLength(1000);
        builder.Property(e => e.EmailedTo).HasMaxLength(1000);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}
