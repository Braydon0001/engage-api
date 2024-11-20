namespace Engage.Persistence.Configurations;

public class TrainingFacilitatorConfiguration : IEntityTypeConfiguration<TrainingFacilitator>
{
    public void Configure(EntityTypeBuilder<TrainingFacilitator> builder)
    {
        builder.HasKey(e => new { e.EmployeeId, e.TrainingId })
            .IsClustered(false);

        builder.HasOne(x => x.Employee)
            .WithMany(c => c.TrainingFacilitators)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.Training)
            .WithMany(c => c.TrainingFacilitators)
            .HasForeignKey(x => x.TrainingId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.Property(e => e.TotalCost)
                   .HasComputedColumnSql("DirectCost + AdditionalCost");

        builder.Property(e => e.Note)
            .HasMaxLength(300);
    }
}
