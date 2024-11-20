// auto-generated
namespace Engage.Application.Services.ProductTransactionTypes.Queries;

public class ProductTransactionTypeOption : IMapFrom<ProductTransactionType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductTransactionType, ProductTransactionTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductTransactionTypeId));
    }
}