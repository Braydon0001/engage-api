namespace Engage.Application.Services.IncidentSkus.Models;

public class IncidentSkuDto : IMapFrom<IncidentSku>
{
    public int Id { get; set; }
    public int IncidentSkuTypeId { get; set; }
    public string IncidentSkuTypeName { get; set; }
    public int IncidentSkuStatusId { get; set; }
    public string IncidentSkuStatusName { get; set; }
    public int DCProductId { get; set; }
    public string DCProductName { get; set; }
    public string Note { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IncidentSku, IncidentSkuDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.IncidentSkuId));
    }
}
