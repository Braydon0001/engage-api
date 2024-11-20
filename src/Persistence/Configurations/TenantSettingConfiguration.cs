using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class TenantSettingConfiguration : IEntityTypeConfiguration<TenantSetting>
    {
        public void Configure(EntityTypeBuilder<TenantSetting> builder)
        {
            builder.HasIndex(e => new { e.SettingId })
                   .IsUnique()
                   .IsClustered(false);

            builder.Property(e => e.Value)
                   .HasMaxLength(200);

            
        }
    }
}
