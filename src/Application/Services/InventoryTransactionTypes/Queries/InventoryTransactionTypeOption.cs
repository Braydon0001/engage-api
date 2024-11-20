// auto-generated
namespace Engage.Application.Services.InventoryTransactionTypes.Queries;

public class InventoryTransactionTypeOption : IMapFrom<InventoryTransactionType>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryTransactionType, InventoryTransactionTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryTransactionTypeId));
    }
}