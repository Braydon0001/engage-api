// auto-generated
namespace Engage.Application.Services.ProductTransactionTypes.Queries;

public class ProductTransactionTypeDto : IMapFrom<ProductTransactionType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsPositive { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductTransactionType, ProductTransactionTypeDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductTransactionTypeId));
    }
}
