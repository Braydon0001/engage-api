namespace Engage.Application.Services.StorePOSFreezerQuestions.Models;

public class StorePOSFreezerQuestionVm : IMapFrom<StorePOSFreezerQuestion>
{
    public int Id { get; set; }
    public OptionDto StoreId { get; set; }
    public OptionDto StorePOSTypeId { get; set; }
    public OptionDto StorePOSFreezerTypeId { get; set; }
    public bool IsWobblers { get; set; }
    public string WobblersComment { get; set; }
    public bool IsFreezerDecals { get; set; }
    public string FreezerDecalsComment { get; set; }
    public bool IsShelfTalker { get; set; }
    public string ShelfTalkerComment { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StorePOSFreezerQuestion, StorePOSFreezerQuestionVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StorePOSFreezerQuestionId))
            .ForMember(d => d.StoreId, opt => opt.MapFrom(s => new OptionDto(s.StoreId, s.Store.Name)))
            .ForMember(d => d.StorePOSTypeId, opt => opt.MapFrom(s => new OptionDto(s.StorePOSTypeId, s.StorePOSType.Name)))
            .ForMember(d => d.StorePOSFreezerTypeId, opt => opt.MapFrom(s => new OptionDto(s.StorePOSFreezerTypeId, s.StorePOSFreezerType.Name)));
    }
}
