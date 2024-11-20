namespace Engage.Persistence.Configurations;

public class EmployeeWorkRoleContactConfiguration : IEntityTypeConfiguration<EmployeeWorkRoleContact>
{
    public void Configure(EntityTypeBuilder<EmployeeWorkRoleContact> builder)
    {
        builder.Property(e => e.EmailAddress)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(e => e.FirstName)
           .HasMaxLength(120)
           .IsRequired();

        builder.Property(e => e.LastName)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(e => e.FullName)
            .HasComputedColumnSql("concat(FirstName,' ',LastName)");

        builder.Property(e => e.MiddleName)
            .HasMaxLength(120);

        builder.Property(e => e.MobilePhone)
            .HasMaxLength(30);

        builder.Property(e => e.Description)
            .HasMaxLength(200);

        builder.Property(e => e.Title)
            .HasMaxLength(120);
    }
}
