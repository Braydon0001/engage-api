namespace Engage.Application.Services.StoreConceptAttributeOptions.Models;

public class StoreConceptAttributeOptionDto : IMapFrom<StoreConceptAttributeOption>
{

    public StoreConceptAttributeOptionDto()
    {
    }

    public StoreConceptAttributeOptionDto(int storeConceptAttributeOptionId, int storeConceptAttributeId, string option)
    {
        Id = storeConceptAttributeOptionId;
        StoreConceptAttributeId = storeConceptAttributeId;
        Option = option;
    }

    public int Id { get; set; }
    public int StoreConceptAttributeId { get; set; }
    public string Option { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<StoreConceptAttributeOption, StoreConceptAttributeOptionDto>()
           .ForMember(d => d.Id, opt => opt.MapFrom(s => s.StoreConceptAttributeOptionId));
    }
}
