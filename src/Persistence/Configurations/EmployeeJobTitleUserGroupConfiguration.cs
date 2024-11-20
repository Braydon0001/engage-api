namespace Engage.Persistence.Configurations;

public class EmployeeJobTitleUserGroupConfiguration : IEntityTypeConfiguration<EmployeeJobTitleUserGroup>
{
    public void Configure(EntityTypeBuilder<EmployeeJobTitleUserGroup> builder)
    {
        builder.HasIndex(e => new { e.EmployeeJobTitleId, e.UserGroupId })
                   .IsUnique()
                   .IsClustered(false);
    }
}
