// auto-generated
namespace Engage.Application.Services.ProductSystemStatuses.Queries;

public class ProductSystemStatusOption : IMapFrom<ProductSystemStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductSystemStatus, ProductSystemStatusOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductSystemStatusId));
    }
}