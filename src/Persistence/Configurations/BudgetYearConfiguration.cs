namespace Engage.Persistence.Configurations;

public class BudgetYearConfiguration : IEntityTypeConfiguration<BudgetYear>
{
    public void Configure(EntityTypeBuilder<BudgetYear> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(20)
            .IsRequired();
    }
}
