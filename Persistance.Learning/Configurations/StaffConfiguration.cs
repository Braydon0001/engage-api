using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Learning.Configurations
{
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.Property(e => e.Name)
                .HasMaxLength(200);

            builder.Property(e => e.Surname)
                .HasMaxLength(200);

            builder.Property(e => e.IdentityNumber)
                .HasMaxLength(200);

            builder.Property(e => e.Cellphone)
                .HasMaxLength(15);

            builder.Property(e => e.Email)
                .HasMaxLength(200);

        }
    }
}
