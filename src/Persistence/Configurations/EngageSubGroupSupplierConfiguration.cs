using Engage.Domain.Entities.LinkEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class EngageSubGroupSupplierConfiguration : IEntityTypeConfiguration<EngageSubGroupSupplier>
    {
        public void Configure(EntityTypeBuilder<EngageSubGroupSupplier> builder)
        {
            builder.HasKey(e => new { e.EngageSubGroupId, e.SupplierId,  })
                .IsClustered(false);

            builder.HasOne(x => x.EngageSubGroup)
                .WithMany(e => e.EngageSubGroupSuppliers)
                .HasForeignKey(x => x.EngageSubGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.Supplier)
                .WithMany(e => e.EngageSubGroupSuppliers)
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            
        }
    }
}
