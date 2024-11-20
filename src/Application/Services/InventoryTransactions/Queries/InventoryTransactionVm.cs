// auto-generated
using Engage.Application.Services.InventoryTransactionTypes.Queries;
using Engage.Application.Services.InventoryTransactionStatuses.Queries;
using Engage.Application.Services.Inventories.Queries;
using Engage.Application.Services.InventoryWarehouses.Queries;

namespace Engage.Application.Services.InventoryTransactions.Queries;

public class InventoryTransactionVm : IMapFrom<InventoryTransaction>
{
    public int Id { get; set; }
    public InventoryTransactionTypeOption InventoryTransactionTypeId { get; set; }
    public InventoryTransactionStatusOption InventoryTransactionStatusId { get; set; }
    public InventoryOption InventoryId { get; set; }
    public InventoryWarehouseOption InventoryWarehouseId { get; set; }
    public float Quantity { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Note { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryTransaction, InventoryTransactionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryTransactionId))
               .ForMember(d => d.InventoryTransactionTypeId, opt => opt.MapFrom(s => s.InventoryTransactionType))
               .ForMember(d => d.InventoryTransactionStatusId, opt => opt.MapFrom(s => s.InventoryTransactionStatus))
               .ForMember(d => d.InventoryId, opt => opt.MapFrom(s => s.Inventory))
               .ForMember(d => d.InventoryWarehouseId, opt => opt.MapFrom(s => s.InventoryWarehouse));
    }
}
