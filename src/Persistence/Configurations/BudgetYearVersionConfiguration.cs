namespace Engage.Persistence.Configurations;

class BudgetYearVersionConfiguration : IEntityTypeConfiguration<BudgetYearVersion>
{
    public void Configure(EntityTypeBuilder<BudgetYearVersion> builder)
    {
        builder.HasKey(e => new { e.BudgetYearId, e.BudgetVersionId })
            .IsClustered(true);

        builder.HasOne(x => x.BudgetVersion)
            .WithMany(s => s.BudgetYearVersions)
            .HasForeignKey(x => x.BudgetVersionId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.BudgetYear)
            .WithMany(s => s.BudgetYearVersions)
            .HasForeignKey(x => x.BudgetYearId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}
