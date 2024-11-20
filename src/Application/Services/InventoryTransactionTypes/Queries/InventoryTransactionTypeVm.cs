// auto-generated
namespace Engage.Application.Services.InventoryTransactionTypes.Queries;

public class InventoryTransactionTypeVm : IMapFrom<InventoryTransactionType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsPositive { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryTransactionType, InventoryTransactionTypeVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryTransactionTypeId));
    }
}
