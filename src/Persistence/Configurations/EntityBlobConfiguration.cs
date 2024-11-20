using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class EntityBlobConfiguration : IEntityTypeConfiguration<EntityBlob>
    {
        public void Configure(EntityTypeBuilder<EntityBlob> builder)
        {
            builder.Property(e => e.FolderName)
                   .HasMaxLength(2000)
                   .IsRequired();
            
            builder.Property(e => e.OriginalFileName)
                   .HasMaxLength(2000)
                   .IsRequired();
            
            builder.Property(e => e.FileName)
                   .HasMaxLength(2000)
                   .IsRequired();
            
            builder.Property(e => e.Url)
                   .HasMaxLength(2000)
                   .IsRequired();            
        }
    }

    public class ClaimBlobConfiguration : IEntityTypeConfiguration<ClaimBlob>
    {
        public void Configure(EntityTypeBuilder<ClaimBlob> builder)
        {
            
        }
    }
}
