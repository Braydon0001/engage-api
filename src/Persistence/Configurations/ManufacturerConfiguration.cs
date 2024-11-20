namespace Engage.Persistence.Configurations;

public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
{
    public void Configure(EntityTypeBuilder<Manufacturer> builder)
    {
        builder.Property(e => e.Name)
               .IsRequired()
               .HasMaxLength(120);
        
        builder.Property(e => e.AccountNumber)               
               .HasMaxLength(30);
    }
}
