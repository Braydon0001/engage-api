using Engage.Domain.Entities.LinkEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class SupplierSupplierTypeConfiguration : IEntityTypeConfiguration<SupplierSupplierType>
    {
        public void Configure(EntityTypeBuilder<SupplierSupplierType> builder)
        {
            builder.HasKey(e => new { e.SupplierId, e.SupplierTypeId })
                .IsClustered(false);

            builder.HasOne(x => x.Supplier)
                .WithMany(e => e.SupplierSupplierTypes)
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.SupplierType)
                .WithMany(e => e.SupplierSupplierTypes)
                .HasForeignKey(x => x.SupplierTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
