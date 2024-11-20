// auto-generated
namespace Engage.Persistence.Configurations;

public class NPrintingBatchConfiguration : IEntityTypeConfiguration<NPrintingBatch>
{
    public void Configure(EntityTypeBuilder<NPrintingBatch> builder)
    {
        builder.Property(e => e.NPrintingBatchId);
        builder.Property(e => e.WebFileCategoryId);
        builder.Property(e => e.FileTypeId);
        builder.Property(e => e.Directory).IsRequired().HasMaxLength(1000);
        builder.Property(e => e.Report).IsRequired().HasMaxLength(100);
        builder.Property(e => e.DisplayName).IsRequired().HasMaxLength(100);
    }
}