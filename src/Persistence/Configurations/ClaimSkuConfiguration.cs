using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class ClaimSkuConfiguration : IEntityTypeConfiguration<ClaimSku>
    {
        public void Configure(EntityTypeBuilder<ClaimSku> builder)
        {
            builder.HasIndex(e => new { e.ClaimId, e.DCProductId })
                   .IsUnique()
                   .IsClustered(false); 

            builder.Property(e => e.TotalAmount)
                   .HasComputedColumnSql("Amount + VatAmount");

            builder.Property(e => e.Note)
                   .HasMaxLength(300);          
        }
    }
}
