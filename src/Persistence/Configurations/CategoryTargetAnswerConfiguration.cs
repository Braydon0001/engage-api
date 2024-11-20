namespace Engage.Persistence.Configurations;

public class CategoryTargetAnswerConfiguration : IEntityTypeConfiguration<CategoryTargetAnswer>
{
    public void Configure(EntityTypeBuilder<CategoryTargetAnswer> builder)
    {
        builder.Property(e => e.CategoryTargetAnswerId).IsRequired();
        builder.Property(e => e.CategoryTargetId).IsRequired();
        builder.Property(e => e.Target).IsRequired();
        builder.Property(e => e.CategoryTargetTypeId);
        builder.Property(e => e.Available).IsRequired();
        builder.Property(e => e.Occupied).IsRequired();
    }
}