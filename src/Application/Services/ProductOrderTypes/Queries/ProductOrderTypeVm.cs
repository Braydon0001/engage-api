
namespace Engage.Application.Services.ProductOrderTypes.Queries;

public class ProductOrderTypeVm : IMapFrom<ProductOrderType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderType, ProductOrderTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductOrderTypeId));
    }
}
