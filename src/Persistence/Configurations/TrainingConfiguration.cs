namespace Engage.Persistence.Configurations;

public class TrainingConfiguration : IEntityTypeConfiguration<Training>
{
    public void Configure(EntityTypeBuilder<Training> builder)
    {
        builder.Property(e => e.Name)
            .HasMaxLength(120);

        builder.Property(e => e.Site)
            .HasMaxLength(120);

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

        builder.Property(e => e.Files).HasColumnType("json");
    }
}
