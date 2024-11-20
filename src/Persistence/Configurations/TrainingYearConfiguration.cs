using Engage.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Engage.Persistence.Configurations;

public class TrainingYearConfiguration : IEntityTypeConfiguration<TrainingYear>
{
    public void Configure(EntityTypeBuilder<TrainingYear> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(20)
            .IsRequired();
    }
}
