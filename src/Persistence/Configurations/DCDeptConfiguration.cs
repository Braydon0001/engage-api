using Engage.Domain.Entities.LinkEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class DCDeptConfiguration : IEntityTypeConfiguration<DCDept>
    {
        public void Configure(EntityTypeBuilder<DCDept> builder)
        {
            builder.HasKey(e => new { e.DistributionCenterId, e.DCDepartmentId })
                .IsClustered(false);

            builder.HasOne(x => x.DistributionCenter)
                .WithMany(d => d.DCDepts)
                .HasForeignKey(x => x.DistributionCenterId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.DCDepartment)
                .WithMany(d => d.DCDepts)
                .HasForeignKey(x => x.DCDepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
