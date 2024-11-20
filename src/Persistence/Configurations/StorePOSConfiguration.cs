using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    class StorePOSConfiguration : IEntityTypeConfiguration<StorePOS>
    {
        public void Configure(EntityTypeBuilder<StorePOS> builder)
        {
            builder.HasIndex(e => new { e.StoreId, e.StorePOSTypeId })
                    .IsUnique();
        }
    }
}
