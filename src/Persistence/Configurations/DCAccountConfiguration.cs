using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class DCAccountConfiguration : IEntityTypeConfiguration<DCAccount>
    {
        public void Configure(EntityTypeBuilder<DCAccount> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(e => e.AccountNumber)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(e => e.Description)
                .HasMaxLength(220);
        }
    }
}
