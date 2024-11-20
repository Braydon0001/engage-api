namespace Engage.Persistence.Configurations;

public class WebFileEmployeeDivisionConfiguration : IEntityTypeConfiguration<WebFileEmployeeDivision>
{
    public void Configure(EntityTypeBuilder<WebFileEmployeeDivision> builder)
    {
        builder.Property(e => e.EmployeeDivisionId).IsRequired();

        // Multi-column index

        builder.HasIndex(e => new { e.WebFileId, e.EmployeeDivisionId }).IsUnique();
    }
}
