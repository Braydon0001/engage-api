// auto-generated
namespace Engage.Persistence.Configurations;

public class WebPageEmployeeConfiguration : IEntityTypeConfiguration<WebPageEmployee>
{
    public void Configure(EntityTypeBuilder<WebPageEmployee> builder)
    {
        builder.Property(e => e.WebPageEmployeeId).IsRequired();
        builder.Property(e => e.EmployeeId).IsRequired();
        builder.Property(e => e.WebPageId).IsRequired();
        builder.Property(e => e.ViewDate).IsRequired();
    }
}