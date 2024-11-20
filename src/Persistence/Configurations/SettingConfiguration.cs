using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class SettingConfiguration : IEntityTypeConfiguration<Setting>
    {
        public void Configure(EntityTypeBuilder<Setting> builder)
        {
            builder.HasIndex(e => e.Name)
                   .IsUnique()
                   .IsClustered(false);
            
            builder.Property(e => e.Name)
                   .HasMaxLength(100);
        }
    }
}
