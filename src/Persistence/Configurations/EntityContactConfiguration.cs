namespace Engage.Persistence.Configurations;

public class EntityContactConfiguration : IEntityTypeConfiguration<EntityContact>
{
    public void Configure(EntityTypeBuilder<EntityContact> builder)
    {
        builder.Property(e => e.EmailAddress1)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.FirstName)
           .HasMaxLength(120)
           .IsRequired();

        builder.Property(e => e.LastName)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(e => e.FullName)
            .HasComputedColumnSql("concat(FirstName,' ',LastName)");

        builder.Property(e => e.MiddleName)
            .HasMaxLength(120);

        builder.Property(e => e.MobilePhone)
            .HasMaxLength(30);

        builder.Property(e => e.Description)
            .HasMaxLength(200);

        builder.Property(e => e.Files).HasColumnType("json");
    }
}

public class EngageRegionContactConfiguration : IEntityTypeConfiguration<EngageRegionContact>
{
    public void Configure(EntityTypeBuilder<EngageRegionContact> builder)
    {
    }
}

public class StoreContactConfiguration : IEntityTypeConfiguration<StoreContact>
{
    public void Configure(EntityTypeBuilder<StoreContact> builder)
    {
    }
}

public class SupplierContactConfiguration : IEntityTypeConfiguration<SupplierContact>
{
    public void Configure(EntityTypeBuilder<SupplierContact> builder)
    {
    }
}
