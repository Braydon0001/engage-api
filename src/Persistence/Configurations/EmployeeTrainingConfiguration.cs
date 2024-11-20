namespace Engage.Persistence.Configurations;

public class EmployeeTrainingConfiguration : IEntityTypeConfiguration<EmployeeTraining>
{
    public void Configure(EntityTypeBuilder<EmployeeTraining> builder)
    {
        builder.HasKey(e => new { e.EmployeeId, e.TrainingId })
            .IsClustered(false);

        builder.HasOne(x => x.Employee)
            .WithMany(c => c.EmployeeTrainings)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(x => x.Training)
            .WithMany(c => c.EmployeeTrainings)
            .HasForeignKey(x => x.TrainingId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.Property(e => e.TotalCost)
                   .HasComputedColumnSql("DirectCost + AdditionalCost + AccommodationCost + CarHireCost + CateringCost + FlightsCost + FuelCost + StationeryCost + VenueCost + OtherCost");

        builder.Property(e => e.AccommodationCost)
                .HasDefaultValue(0);

        builder.Property(e => e.CarHireCost)
            .HasDefaultValue(0);

        builder.Property(e => e.CateringCost)
            .HasDefaultValue(0);

        builder.Property(e => e.FlightsCost)
            .HasDefaultValue(0);

        builder.Property(e => e.FuelCost)
            .HasDefaultValue(0);

        builder.Property(e => e.StationeryCost)
            .HasDefaultValue(0);

        builder.Property(e => e.VenueCost)
            .HasDefaultValue(0);

        builder.Property(e => e.OtherCost)
            .HasDefaultValue(0);

        builder.Property(e => e.Note)
            .HasMaxLength(300);
    }
}
