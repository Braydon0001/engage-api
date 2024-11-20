namespace Engage.Persistence.Configurations;

public class DCProductConfiguration : IEntityTypeConfiguration<DCProduct>
{
    public void Configure(EntityTypeBuilder<DCProduct> builder)
    {
        builder.Property(e => e.Name).IsRequired().HasMaxLength(220);
        builder.Property(e => e.Code).IsRequired().HasMaxLength(30);
        builder.Property(e => e.Size).IsRequired();
        builder.Property(e => e.PackSize).IsRequired();
        builder.Property(e => e.EANNumber).HasMaxLength(20);
        builder.Property(e => e.SubWarehouse).HasMaxLength(20);
        builder.Property(e => e.Files).HasColumnType("json");

        builder.HasOne(x => x.EngageVariantProduct)
            .WithMany(e => e.DCProducts)
            .HasForeignKey(x => x.EngageVariantProductId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
