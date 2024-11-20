namespace Engage.Persistence.Configurations;

class BudgetPeriodConfiguration : IEntityTypeConfiguration<BudgetPeriod>
{
    public void Configure(EntityTypeBuilder<BudgetPeriod> builder)
    {
        builder.Property(x => x.No)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.HasOne(x => x.BudgetYear)
            .WithMany(s => s.BudgetPeriods)
            .HasForeignKey(x => x.BudgetYearId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
