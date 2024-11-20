namespace Engage.Application.Services.ProductOrderLineTypes.Queries;

public class ProductOrderLineTypeDto : IMapFrom<ProductOrderLineType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderLineType, ProductOrderLineTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductOrderLineTypeId));
    }
}
