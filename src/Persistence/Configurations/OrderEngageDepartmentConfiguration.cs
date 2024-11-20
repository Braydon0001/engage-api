using Engage.Domain.Entities.LinkEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class OrderEngageDepartmentConfiguration : IEntityTypeConfiguration<OrderEngageDepartment>
    {
        public void Configure(EntityTypeBuilder<OrderEngageDepartment> builder)
        {
            builder.HasKey(e => new { e.OrderId, e.EngageDepartmentId })
                .IsClustered(false);

            builder.HasOne(e => e.Order)
                .WithMany(e => e.OrderEngageDepartments)
                .HasForeignKey(e => e.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.EngageDepartment)
                .WithMany(e => e.OrderEngageDepartments)
                .HasForeignKey(e => e.EngageDepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
