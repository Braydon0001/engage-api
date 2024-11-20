// auto-generated
namespace Engage.Persistence.Configurations;

public class WebFileEmployeeConfiguration : IEntityTypeConfiguration<WebFileEmployee>
{
    public void Configure(EntityTypeBuilder<WebFileEmployee> builder)
    {
        builder.Property(e => e.EmployeeId).IsRequired();

        // Multi-column indexes 

        builder.HasIndex(e => new { e.WebFileId, e.EmployeeId }).IsUnique();
    }
}