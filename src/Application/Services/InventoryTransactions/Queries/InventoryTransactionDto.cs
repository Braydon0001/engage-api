// auto-generated
namespace Engage.Application.Services.InventoryTransactions.Queries;

public class InventoryTransactionDto : IMapFrom<InventoryTransaction>
{
    public int Id { get; set; }
    public int InventoryTransactionTypeId { get; set; }
    public string InventoryTransactionTypeName { get; set; }
    public int InventoryTransactionStatusId { get; set; }
    public string InventoryTransactionStatusName { get; set; }
    public int InventoryId { get; set; }
    public string InventoryName { get; set; }
    public int InventoryWarehouseId { get; set; }
    public string InventoryWarehouseName { get; set; }
    public float Quantity { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Note { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<InventoryTransaction, InventoryTransactionDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.InventoryTransactionId));
    }
}
