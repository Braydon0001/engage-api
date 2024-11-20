// auto-generated
namespace Engage.Application.Services.ProductReasons.Queries;

public class ProductReasonOption : IMapFrom<ProductReason>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductReason, ProductReasonOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductReasonId));
    }
}