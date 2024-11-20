
namespace Engage.Application.Services.ProductOrderLineStatuses.Queries;

public class ProductOrderLineStatusVm : IMapFrom<ProductOrderLineStatus>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductOrderLineStatus, ProductOrderLineStatusVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductOrderLineStatusId));
    }
}
