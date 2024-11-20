using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class VatPeriodConfiguration : IEntityTypeConfiguration<VatPeriod>
    {
        public void Configure(EntityTypeBuilder<VatPeriod> builder)
        {
          
        }
    }
}
