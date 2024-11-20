using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Learning.Configurations
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.Property(e => e.Name)
                .HasMaxLength(200);

            builder.Property(e => e.Description)
                .HasMaxLength(1024);

        }
    }
}
