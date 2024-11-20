namespace Engage.Application.Services.SubContractorBrands.Queries;

public class SubContractorBrandDto : IMapFrom<SubContractorBrand>
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string SupplierName { get; set; }
    public string EngageBrandName { get; set; }
    public string EngageRegionName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SubContractorBrand, SubContractorBrandDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SubContractorBrandId));
    }
}
