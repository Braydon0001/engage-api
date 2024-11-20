namespace Engage.Application.Services.StorePOSFreezerQuestions.Models;

public class StorePOSFreezerQuestionDto : IMapFrom<StorePOSFreezerQuestion>
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public int StorePOSTypeId { get; set; }
    public string StorePOSTypeName { get; set; }
    public int StorePOSFreezerTypeId { get; set; }
    public string StorePOSFreezerTypeName { get; set; }
    public bool IsWobblers { get; set; }
    public string WobblersComment { get; set; }
    public bool IsFreezerDecals { get; set; }
    public string FreezerDecalsComment { get; set; }
    public bool IsShelfTalker { get; set; }
    public string ShelfTalkerComment { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StorePOSFreezerQuestion, StorePOSFreezerQuestionDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StorePOSFreezerQuestionId))
            .ForMember(d => d.StorePOSTypeName, opt => opt.MapFrom(s => s.StorePOSType.Name))
            .ForMember(d => d.StorePOSFreezerTypeName, opt => opt.MapFrom(s => s.StorePOSFreezerType.Name));
    }
}
