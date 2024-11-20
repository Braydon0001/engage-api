using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Engage.Persistence.Configurations
{
    class StorePOSQuestionConfiguration : IEntityTypeConfiguration<StorePOSQuestion>
    {
        public void Configure(EntityTypeBuilder<StorePOSQuestion> builder)
        {
            builder.HasIndex(e => new { e.StoreId, e.StorePOSTypeId })
                    .IsUnique();

            builder.Property(e => e.FridgeDecalsComment)
                   .IsRequired()
                   .HasMaxLength(1000);
            
            builder.Property(e => e.FloorDecalsComment)
                   .IsRequired()
                   .HasMaxLength(1000);
            
            builder.Property(e => e.FSUDecalsComment)
                   .IsRequired()
                   .HasMaxLength(1000);
            
            builder.Property(e => e.FSUDecalsPaidComment)
                   .IsRequired()
                   .HasMaxLength(1000);
            
            builder.Property(e => e.ShelfStripsComment)
                   .IsRequired()
                   .HasMaxLength(1000);
            
            builder.Property(e => e.AisleBladesComment)
                   .IsRequired()
                   .HasMaxLength(1000);
            
            builder.Property(e => e.StandeeComment)
                   .IsRequired()
                   .HasMaxLength(1000);
            
            builder.Property(e => e.EntryBoxComment)
                   .IsRequired()
                   .HasMaxLength(1000);
            
            builder.Property(e => e.BaseWrapComment)
                   .IsRequired()
                   .HasMaxLength(1000);
            
            builder.Property(e => e.GondolaHeaderComment)
                   .IsRequired()
                   .HasMaxLength(1000);
            
            builder.Property(e => e.HangingMobilesComment)
                   .IsRequired()
                   .HasMaxLength(1000);
            
            builder.Property(e => e.PollUpBannerComment)
                   .IsRequired()
                   .HasMaxLength(1000);
            
            builder.Property(e => e.ParaciteUnitsComment)
                   .IsRequired()
                   .HasMaxLength(1000);
            
            builder.Property(e => e.SensorSleavesComment)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(e => e.NeckTagsComment)
                   .IsRequired()
                   .HasMaxLength(1000);




        }
    }
}
