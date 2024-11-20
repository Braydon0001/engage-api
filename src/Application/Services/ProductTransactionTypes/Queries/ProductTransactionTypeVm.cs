// auto-generated
namespace Engage.Application.Services.ProductTransactionTypes.Queries;

public class ProductTransactionTypeVm : IMapFrom<ProductTransactionType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsPositive { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductTransactionType, ProductTransactionTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductTransactionTypeId));
    }
}
