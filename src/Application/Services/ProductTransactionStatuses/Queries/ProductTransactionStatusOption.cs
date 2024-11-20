// auto-generated
namespace Engage.Application.Services.ProductTransactionStatuses.Queries;

public class ProductTransactionStatusOption : IMapFrom<ProductTransactionStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductTransactionStatus, ProductTransactionStatusOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductTransactionStatusId));
    }
}