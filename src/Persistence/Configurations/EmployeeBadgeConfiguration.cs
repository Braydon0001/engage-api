using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engage.Persistence.Configurations
{
    public class EmployeeBadgeConfiguration : IEntityTypeConfiguration<EmployeeBadge>
    {
        public void Configure(EntityTypeBuilder<EmployeeBadge> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(120);
        }
    }
}
