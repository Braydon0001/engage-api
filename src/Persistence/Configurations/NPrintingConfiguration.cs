// auto-generated
namespace Engage.Persistence.Configurations;

public class NPrintingConfiguration : IEntityTypeConfiguration<NPrinting>
{
    public void Configure(EntityTypeBuilder<NPrinting> builder)
    {
        builder.Property(e => e.NPrintingId);
        builder.Property(e => e.NPrintingBatchId);
        builder.Property(e => e.FileName).IsRequired().HasMaxLength(1000);
        builder.Property(e => e.ProcessedDate);
        builder.Property(e => e.Error).HasMaxLength(1000);
    }
}