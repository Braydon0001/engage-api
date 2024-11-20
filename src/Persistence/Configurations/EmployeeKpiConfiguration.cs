using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engage.Persistence.Configurations
{
    public class EmployeeKpiConfiguration : IEntityTypeConfiguration<EmployeeKpi>
    {
        public void Configure(EntityTypeBuilder<EmployeeKpi> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(120);
        }
    }
}
