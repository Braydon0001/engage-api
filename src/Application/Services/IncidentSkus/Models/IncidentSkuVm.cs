namespace Engage.Application.Services.IncidentSkus.Models;

public class IncidentSkuVm : IMapFrom<IncidentSku>
{
    public int Id { get; set; }
    public OptionDto IncidentSkuTypeId { get; set; }
    public OptionDto IncidentSkuStatusId { get; set; }
    public OptionDto DCProductId { get; set; }
    public string Note { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IncidentSku, IncidentSkuVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.IncidentSkuId))
            .ForMember(d => d.IncidentSkuTypeId, opt => opt.MapFrom(s => new OptionDto(s.IncidentSkuTypeId, s.IncidentSkuType.Name)))
            .ForMember(d => d.IncidentSkuStatusId, opt => opt.MapFrom(s => new OptionDto(s.IncidentSkuStatusId, s.IncidentSkuStatus.Name)))
            .ForMember(d => d.DCProductId, opt => opt.MapFrom(s => new OptionDto(s.DCProductId, s.DCProduct.Name)));
    }
}
