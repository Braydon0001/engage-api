// auto-generated
namespace Engage.Application.Services.ProductReasons.Queries;

public class ProductReasonVm : IMapFrom<ProductReason>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductReason, ProductReasonVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductReasonId));
    }
}
