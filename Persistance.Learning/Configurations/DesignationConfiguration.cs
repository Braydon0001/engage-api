using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Learning.Configurations
{
    public class DesignationConfiguration : IEntityTypeConfiguration<Designation>
    {
        public void Configure(EntityTypeBuilder<Designation> builder)
        {
            builder.Property(e => e.Name)
                .HasMaxLength(200);

            builder.Property(e => e.Description)
                .HasMaxLength(1024);

        }
    }
}
