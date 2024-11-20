// auto-generated
namespace Engage.Application.Services.InventoryTransactionStatuses.Queries;

public class InventoryTransactionStatusDto : IMapFrom<InventoryTransactionStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryTransactionStatus, InventoryTransactionStatusDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryTransactionStatusId));
    }
}
