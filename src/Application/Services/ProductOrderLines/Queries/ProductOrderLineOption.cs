namespace Engage.Application.Services.ProductOrderLines.Queries;

public class ProductOrderLineOption : IMapFrom<ProductOrderLine>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderLine, ProductOrderLineOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductOrderLineId));
    }
}