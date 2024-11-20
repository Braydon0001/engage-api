
namespace Engage.Application.Services.ProductOrderLineTypes.Queries;

public class ProductOrderLineTypeVm : IMapFrom<ProductOrderLineType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderLineType, ProductOrderLineTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductOrderLineTypeId));
    }
}
