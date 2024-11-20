using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class StakeholderConfiguration : IEntityTypeConfiguration<Stakeholder>
    {
        public void Configure(EntityTypeBuilder<Stakeholder> builder)
        {

        }
    }
}
