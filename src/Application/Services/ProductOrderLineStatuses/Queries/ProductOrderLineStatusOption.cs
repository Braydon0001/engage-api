namespace Engage.Application.Services.ProductOrderLineStatuses.Queries;

public class ProductOrderLineStatusOption : IMapFrom<ProductOrderLineStatus>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderLineStatus, ProductOrderLineStatusOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductOrderLineStatusId));
    }
}