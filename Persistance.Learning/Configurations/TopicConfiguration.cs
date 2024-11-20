using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Learning.Configurations
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.Property(e => e.TopicName)
                .HasMaxLength(200);

            builder.Property(e => e.ModuleName)
                .HasMaxLength(200);
        }
    }
}
