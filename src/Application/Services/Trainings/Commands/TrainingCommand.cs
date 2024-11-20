namespace Engage.Application.Services.Trainings.Commands;

public class TrainingCommand : IMapTo<Training>
{
    public string Name { get; set; }
    public int? TrainingProviderId { get; set; }
    public int TrainingTypeId { get; set; }
    public int? EngageRegionId { get; set; }
    public int? TrainingCategoryId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsInternalTraining { get; set; }
    public string Site { get; set; }
    public string Note { get; set; }
    //public int NoOfParticipants { get; set; }
    public string Duration { get; set; }
    public int? TrainingDurationId { get; set; }
    public decimal DirectCost { get; set; }
    public decimal AdditionalCost { get; set; } = 0;

    public decimal AccommodationCost { get; set; } = 0;
    public decimal CarHireCost { get; set; } = 0;
    public decimal CateringCost { get; set; } = 0;
    public decimal FlightsCost { get; set; } = 0;
    public decimal FuelCost { get; set; } = 0;
    public decimal StationeryCost { get; set; } = 0;
    public decimal VenueCost { get; set; } = 0;
    public decimal OtherCost { get; set; } = 0;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<TrainingCommand, Training>();
    }
}
