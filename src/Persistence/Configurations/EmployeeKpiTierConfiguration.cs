using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engage.Persistence.Configurations
{
    public class EmployeeKpiTierConfiguration : IEntityTypeConfiguration<EmployeeKpiTier>
    {
        public void Configure(EntityTypeBuilder<EmployeeKpiTier> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(120);
        }
    }
}
