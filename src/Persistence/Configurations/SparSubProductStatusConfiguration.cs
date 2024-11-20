namespace Engage.Persistence.Configurations;

public class SparSubProductStatusConfiguration : IEntityTypeConfiguration<SparSubProductStatus>
{
    public void Configure(EntityTypeBuilder<SparSubProductStatus> builder)
    {
        builder.Property(e => e.SparSubProductStatusId).IsRequired();
        builder.Property(e => e.Name).IsRequired().HasMaxLength(120);
    }
}