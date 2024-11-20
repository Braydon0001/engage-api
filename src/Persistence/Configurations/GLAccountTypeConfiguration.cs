using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engage.Persistence.Configurations
{
    class GLAccountTypeConfiguration : IEntityTypeConfiguration<GLAccountType>
    {
        public void Configure(EntityTypeBuilder<GLAccountType> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(100);
        }
    }
}
