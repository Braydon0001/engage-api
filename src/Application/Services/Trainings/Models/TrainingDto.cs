namespace Engage.Application.Services.Trainings.Models;

public class TrainingDto : IMapFrom<Training>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int TrainingProviderId { get; set; }
    public string TrainingProviderName { get; set; }
    public int TrainingTypeId { get; set; }
    public string TrainingTypeName { get; set; }
    public int? EngageRegionId { get; set; }
    public string EngageRegionName { get; set; }
    public int? TrainingCategoryId { get; set; }
    public string TrainingCategoryName { get; set; }
    public int? TrainingDurationId { get; set; }
    public string TrainingDurationName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsInternalTraining { get; set; }
    public string Site { get; set; }
    public string Note { get; set; }
    public int NoOfParticipants { get; set; }
    public string Duration { get; set; }
    public decimal DirectCost { get; set; }
    public decimal AdditionalCost { get; set; }
    public decimal TotalCost { get; set; }

    public decimal AccommodationCost { get; set; }
    public decimal CarHireCost { get; set; }
    public decimal CateringCost { get; set; }
    public decimal FlightsCost { get; set; }
    public decimal FuelCost { get; set; }
    public decimal StationeryCost { get; set; }
    public decimal VenueCost { get; set; }
    public decimal OtherCost { get; set; }

    public bool Disabled { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Training, TrainingDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.TrainingId))
            .ForMember(d => d.TrainingProviderName, opt => opt.MapFrom(s => s.TrainingProvider.Name))
            .ForMember(d => d.TrainingTypeName, opt => opt.MapFrom(s => s.TrainingType.Name))
            .ForMember(d => d.EngageRegionName, opt => opt.MapFrom(s => s.EngageRegion.Name))
            .ForMember(d => d.TrainingCategoryName, opt => opt.MapFrom(s => s.TrainingCategory.Name));
    }
}
