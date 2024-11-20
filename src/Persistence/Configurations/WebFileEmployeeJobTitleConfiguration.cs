// auto-generated
namespace Engage.Persistence.Configurations;

public class WebFileEmployeeJobTitleConfiguration : IEntityTypeConfiguration<WebFileEmployeeJobTitle>
{
    public void Configure(EntityTypeBuilder<WebFileEmployeeJobTitle> builder)
    {
        builder.Property(e => e.EmployeeJobTitleId).IsRequired();

        // Multi-column indexes 

        builder.HasIndex(e => new { e.WebFileId, e.EmployeeJobTitleId }).IsUnique();
    }
}