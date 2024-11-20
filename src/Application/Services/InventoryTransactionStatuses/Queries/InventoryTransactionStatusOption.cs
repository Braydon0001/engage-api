// auto-generated
namespace Engage.Application.Services.InventoryTransactionStatuses.Queries;

public class InventoryTransactionStatusOption : IMapFrom<InventoryTransactionStatus>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryTransactionStatus, InventoryTransactionStatusOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryTransactionStatusId));
    }
}