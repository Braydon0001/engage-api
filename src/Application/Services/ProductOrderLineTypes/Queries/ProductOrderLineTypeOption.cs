namespace Engage.Application.Services.ProductOrderLineTypes.Queries;

public class ProductOrderLineTypeOption : IMapFrom<ProductOrderLineType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderLineType, ProductOrderLineTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductOrderLineTypeId));
    }
}