using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    class SubWarehouseConfiguration : IEntityTypeConfiguration<SubWarehouse>
    {
        public void Configure(EntityTypeBuilder<SubWarehouse> builder)
        {
            builder.Property(e => e.Name)
                   .IsRequired()
                   .HasMaxLength(120);
        }
    }
}
