using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations
{
    public class StoreListConfiguration : IEntityTypeConfiguration<StoreList>
    {
        public void Configure(EntityTypeBuilder<StoreList> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(120);

            builder.Property(e => e.Description)
                .HasMaxLength(220);
        }
    }
}