namespace Engage.Application.Services.ProductOrderStatuses.Queries;

public class ProductOrderStatusDto : IMapFrom<ProductOrderStatus>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderStatus, ProductOrderStatusDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductOrderStatusId));
    }
}
