namespace Engage.Application.Services.ProductOrderTypes.Queries;

public class ProductOrderTypeDto : IMapFrom<ProductOrderType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderType, ProductOrderTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductOrderTypeId));
    }
}
