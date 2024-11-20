// auto-generated
namespace Engage.Persistence.Configurations;

public class EmployeePensionConfiguration : IEntityTypeConfiguration<EmployeePension>
{
    public void Configure(EntityTypeBuilder<EmployeePension> builder)
    {
        builder.Property(e => e.EmployeePensionId).IsRequired();
        builder.Property(e => e.EmployeeId).IsRequired();
        builder.Property(e => e.EmployeePensionSchemeId).IsRequired();
        builder.Property(e => e.EmployeePensionCategoryId).IsRequired();
        builder.Property(e => e.EmployeePensionContributionPercentageId).IsRequired();
        builder.Property(e => e.Files).HasColumnType("json");
    }
}