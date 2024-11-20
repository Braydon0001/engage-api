using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class TargetingConfiguration : IEntityTypeConfiguration<Targeting>
    {
        public void Configure(EntityTypeBuilder<Targeting> builder)
        {            
        }
    }
}
