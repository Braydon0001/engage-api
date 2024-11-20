using Engage.Domain.Entities.LinkEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class StoreStoreDepartmentConfiguration : IEntityTypeConfiguration<StoreStoreDepartment>
    {
        public void Configure(EntityTypeBuilder<StoreStoreDepartment> builder)
        {
            builder.HasKey(e => new { e.StoreId, e.StoreDepartmentId })
                .IsClustered(false);

            builder.HasOne(x => x.Store)
                .WithMany(e => e.StoreStoreDepartments)
                .HasForeignKey(x => x.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.StoreDepartment)
                .WithMany(e => e.StoreDepartments)
                .HasForeignKey(x => x.StoreDepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
