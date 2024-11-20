// auto-generated
namespace Engage.Application.Services.InventoryTransactions.Queries;

public class InventoryTransactionPaginatedListQuery : PaginatedQuery, IRequest<List<InventoryTransactionDto>>
{

}

public class InventoryTransactionPaginatedListHandler : ListQueryHandler, IRequestHandler<InventoryTransactionPaginatedListQuery, List<InventoryTransactionDto>>
{
    public InventoryTransactionPaginatedListHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<List<InventoryTransactionDto>> Handle(InventoryTransactionPaginatedListQuery query, CancellationToken cancellationToken)
    {
        var paginationModels = CreatePaginationModels();

        var queryable = _context.InventoryTransactions.AsQueryable().AsNoTracking();

        var entities = await queryable.Filter(query, paginationModels)
                                      .Sort(query, paginationModels)
                                      .Skip(query.StartRow)
                                      .Take(query.PageSize)
                                      .ProjectTo<InventoryTransactionDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

         return entities;                             
    }
 
    private static Dictionary<string, PaginationProperty> CreatePaginationModels()
    {
        return new Dictionary<string, PaginationProperty> {

            { "id", new PaginationProperty("inventoryTransactionId") },
            { "inventoryTransactionTypeName", new PaginationProperty("InventoryTransactionType.Name") },
            { "inventoryTransactionStatusName", new PaginationProperty("InventoryTransactionStatus.Name") },
            { "inventoryName", new PaginationProperty("Inventory.Name") },
            { "inventoryWarehouseName", new PaginationProperty("InventoryWarehouse.Name") },
            { "quantity", new PaginationProperty("Quantity") },
            { "transactionDate", new PaginationProperty("TransactionDate") },
            { "note", new PaginationProperty("Note") }    

        };
    }
}


