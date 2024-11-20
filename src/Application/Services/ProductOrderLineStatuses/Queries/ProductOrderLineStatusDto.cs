namespace Engage.Application.Services.ProductOrderLineStatuses.Queries;

public class ProductOrderLineStatusDto : IMapFrom<ProductOrderLineStatus>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderLineStatus, ProductOrderLineStatusDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductOrderLineStatusId));
    }
}
