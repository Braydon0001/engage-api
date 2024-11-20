using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engage.Persistence.Configurations
{
    class GLAdjustmentConfiguration : IEntityTypeConfiguration<GLAdjustment>
    {
        public void Configure(EntityTypeBuilder<GLAdjustment> builder)
        {
            builder.Property(x => x.Type)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.GLCode)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.GLDescription)
                .HasMaxLength(100);

            builder.Property(x => x.TransactionDate)
                .IsRequired();

            builder.Property(x => x.DocumentNo)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.DebitValue)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.CreditValue)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(100);

            builder.Property(x => x.Invoice)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Account)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasOne(x => x.Supplier)
                .WithMany(s => s.GLAdjustments)
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(x => x.GLAdjustmentType)
                .WithMany(s => s.GLAdjustments)
                .HasForeignKey(x => x.GLAdjustmentTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
