namespace Engage.Persistence.Configurations;

public class CategoryFileConfiguration : IEntityTypeConfiguration<CategoryFile>
{
    public void Configure(EntityTypeBuilder<CategoryFile> builder)
    {
        builder.Property(e => e.CategoryFileId).IsRequired();
        builder.Property(e => e.CategoryFileTypeId).IsRequired();
        builder.Property(e => e.StoreId);
        builder.Property(e => e.CategoryGroupId);
        builder.Property(e => e.CategorySubGroupId);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Note).HasMaxLength(300);
        builder.Property(e => e.StartDate);
        builder.Property(e => e.EndDate);
        builder.Property(e => e.Files).HasColumnType("json");
        builder.Property(e => e.TargetRule).HasColumnType("json");
    }
}