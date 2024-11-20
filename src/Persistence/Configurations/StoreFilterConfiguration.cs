namespace Engage.Persistence.Configurations;

public class StoreFilterConfiguration : IEntityTypeConfiguration<StoreFilter>
{
    public void Configure(EntityTypeBuilder<StoreFilter> builder)
    {
        builder.Property(e => e.Filter)
               .IsRequired()
               .HasMaxLength(120);

        builder.Property(e => e.AS400)
               .HasMaxLength(120);
    }
}
