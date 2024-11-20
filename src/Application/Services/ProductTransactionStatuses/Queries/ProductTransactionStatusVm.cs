// auto-generated
namespace Engage.Application.Services.ProductTransactionStatuses.Queries;

public class ProductTransactionStatusVm : IMapFrom<ProductTransactionStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProductTransactionStatus, ProductTransactionStatusVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ProductTransactionStatusId));
    }
}
