// auto-generated
namespace Engage.Persistence.Configurations;

public class WebFileConfiguration : IEntityTypeConfiguration<WebFile>
{
    public void Configure(EntityTypeBuilder<WebFile> builder)
    {
        builder.Property(e => e.WebFileId);
        builder.Property(e => e.WebFileCategoryId).IsRequired();
        builder.Property(e => e.FileTypeId).IsRequired();
        builder.Property(e => e.TargetStrategyId).IsRequired();
        builder.Property(e => e.EmployeeId);
        builder.Property(e => e.StoreId);
        builder.Property(e => e.NPrintingId);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.DisplayName).IsRequired().HasMaxLength(100);
        builder.Property(e => e.StartDate).IsRequired();
        builder.Property(e => e.EndDate);
        builder.Property(e => e.Files).HasColumnType("json");
    }
}