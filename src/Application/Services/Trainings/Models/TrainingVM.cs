using Engage.Application.Services.TrainingDurations.Queries;

namespace Engage.Application.Services.Trainings.Models;

public class TrainingVm : IMapFrom<Training>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public OptionDto TrainingProviderId { get; set; }
    public OptionDto TrainingTypeId { get; set; }
    public OptionDto EngageRegionId { get; set; }
    public OptionDto TrainingCategoryId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public TrainingDurationOption TrainingDurationId { get; set; }
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

    public List<JsonFile> Files { get; set; }
    public List<JsonFile> FileRegister { get; set; }
    public List<JsonFile> FileInvoice { get; set; }
    public List<JsonFile> FileReceipt { get; set; }

    public List<JsonFile> FileAccommodationCost { get; set; }
    public List<JsonFile> FileCarHireCost { get; set; }
    public List<JsonFile> FileCateringCost { get; set; }
    public List<JsonFile> FileFlightsCost { get; set; }
    public List<JsonFile> FileFuelCost { get; set; }
    public List<JsonFile> FileStationeryCost { get; set; }
    public List<JsonFile> FileVenueCost { get; set; }
    public List<JsonFile> FileOtherCost { get; set; }

    public ICollection<OptionDto> EmployeeIds { get; set; }
    public ICollection<OptionDto> FacilitatorIds { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Training, TrainingVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.TrainingId))
            //.ForMember(d => d.FileRegister, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "register")))
            //.ForMember(d => d.FileRegister, opt => opt.MapFrom(s => s.TrainingFiles.Where(e => e.TrainingFileTypeId == (int)TrainingFileTypeId.Register).Select(f => f.Files)))
            //.ForMember(d => d.FileInvoice, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "invoice")))
            //.ForMember(d => d.FileReceipt, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "receipt")))
            //.ForMember(d => d.FileAccommodationCost, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "accommodationcost")))
            //.ForMember(d => d.FileCarHireCost, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "carhirecost")))
            //.ForMember(d => d.FileCateringCost, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "cateringcost")))
            //.ForMember(d => d.FileFlightsCost, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "flightscost")))
            //.ForMember(d => d.FileFuelCost, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "fuelcost")))
            //.ForMember(d => d.FileStationeryCost, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "stationerycost")))
            //.ForMember(d => d.FileVenueCost, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "venuecost")))
            //.ForMember(d => d.FileOtherCost, opt => opt.MapFrom(s => s.Files.Where(f => f.Type == "othercost")))
            .ForMember(d => d.TrainingDurationId, opt => opt.MapFrom(s => s.TrainingDuration))
            .ForMember(d => d.TrainingProviderId, opt => opt.MapFrom(s => s.TrainingProviderId.HasValue
                                                                          ? new OptionDto(s.TrainingProviderId.Value, s.TrainingProvider.Name)
                                                                          : null))
            .ForMember(d => d.TrainingTypeId, opt => opt.MapFrom(s => new OptionDto(s.TrainingTypeId, s.TrainingType.Name)))
            .ForMember(d => d.EngageRegionId, opt => opt.MapFrom(s => s.EngageRegionId.HasValue
                                                                          ? new OptionDto(s.EngageRegionId.Value, s.EngageRegion.Name)
                                                                          : null))
            .ForMember(d => d.TrainingCategoryId, opt => opt.MapFrom(s => s.TrainingCategoryId.HasValue
                                                                          ? new OptionDto(s.TrainingCategoryId.Value, s.TrainingCategory.Name)
                                                                          : null));
    }
}
