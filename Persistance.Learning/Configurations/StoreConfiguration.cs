using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Learning.Configurations
{
    public class StoreConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.Property(e => e.StoreName)
                .HasMaxLength(200);

        }
    }
}
