namespace Engage.Persistence.Configurations;

public class WebFileEngageDepartmentConfiguration : IEntityTypeConfiguration<WebFileEngageDepartment>
{
    public void Configure(EntityTypeBuilder<WebFileEngageDepartment> builder)
    {
        builder.Property(e => e.EngageDepartmentId).IsRequired();

        //Multi-column index

        builder.HasIndex(e => new { e.WebFileId, e.EngageDepartmentId }).IsUnique();
    }
}
