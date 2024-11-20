namespace Engage.Persistence.Configurations;

class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.HasIndex(e => new { e.BudgetVersionId, e.BudgetYearId, e.BudgetPeriodId, e.GLAccountId })
            .IsUnique()
            .IsClustered(false);

        builder.HasOne(x => x.GLAccount)
            .WithMany(s => s.Budgets)
            .HasForeignKey(x => x.GLAccountId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.BudgetType)
            .WithMany(s => s.Budgets)
            .HasForeignKey(x => x.BudgetTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.BudgetYear)
            .WithMany(s => s.Budgets)
            .HasForeignKey(x => x.BudgetYearId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.BudgetVersion)
            .WithMany(s => s.Budgets)
            .HasForeignKey(x => x.BudgetVersionId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.BudgetPeriod)
            .WithMany(s => s.Budgets)
            .HasForeignKey(x => x.BudgetPeriodId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.Property(x => x.Value)
            .HasMaxLength(20)
            .IsRequired();
    }
}
