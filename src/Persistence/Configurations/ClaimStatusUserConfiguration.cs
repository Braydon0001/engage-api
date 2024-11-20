using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class ClaimStatusUserConfiguration : IEntityTypeConfiguration<ClaimStatusUser>
    {
        public void Configure(EntityTypeBuilder<ClaimStatusUser> builder)
        {
            builder.HasIndex(e => new { e.ClaimStatusId, e.UserId  })
                   .IsUnique() 
                   .IsClustered(false);          
        }
    }
}
