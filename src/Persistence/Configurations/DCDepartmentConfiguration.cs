using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class DCDepartmentConfiguration : IEntityTypeConfiguration<DCDepartment>
    {
        public void Configure(EntityTypeBuilder<DCDepartment> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(120);
        }
    }
}
