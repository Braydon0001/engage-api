using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engage.Persistence.Configurations
{
    class GLAccountConfiguration : IEntityTypeConfiguration<GLAccount>
    {
        public void Configure(EntityTypeBuilder<GLAccount> builder)
        {
            builder.Property(x => x.Code)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(x => x.GLAccountType)
                .WithMany(s => s.GLAccounts)
                .HasForeignKey(x => x.GLAccountTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.EngageRegion)
                .WithMany(s => s.GlAccounts)
                .HasForeignKey(x => x.EngageRegionId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
