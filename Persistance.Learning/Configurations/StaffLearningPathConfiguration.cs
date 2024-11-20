using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Learning.Configurations
{
    public class StaffLearningPathConfiguration : IEntityTypeConfiguration<StaffLearningPath>
    {
        public void Configure(EntityTypeBuilder<StaffLearningPath> builder)
        {
            builder.Property(e => e.TopicName)
                .HasMaxLength(200);

            builder.Property(e => e.ModuleName)
                .HasMaxLength(200);
        }
    }
}
