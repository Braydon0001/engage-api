namespace Engage.Application.Services.ProductTransactions.Queries;

public class ProductTransactionExcelExportQuery : PaginatedQuery, IRequest<InventoryExcelReportVm<ProductTransactionDto>>
{
    public int EndRow { get; set; }
}
public record ProductTransactionExcelExportHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductTransactionExcelExportQuery, InventoryExcelReportVm<ProductTransactionDto>>
{
    public async Task<InventoryExcelReportVm<ProductTransactionDto>> Handle(ProductTransactionExcelExportQuery query, CancellationToken cancellationToken)
    {
        var paginationModel = ProductTransactionPaginationProps.Create();

        var takeAmount = query.EndRow - query.StartRow;

        var queryable = Context.ProductTransactions.AsQueryable().AsNoTracking();

        var entity = await queryable.Filter(query, paginationModel)
                                    .Sort(query, paginationModel)
                                    .Skip(query.StartRow)
                                    .Take(takeAmount)
                                    .ProjectTo<ProductTransactionDto>(Mapper.ConfigurationProvider)
                                    .ToListAsync(cancellationToken);

        List<string> columns = ["Id",
            "ProductId",
            "ProductName",
            "ProductTransactionTypeId",
            "ProductTransactionTypeName",
            "ProductTransactionStatusId",
            "ProductTransactionStatusName",
            "ProductPeriodId",
            "ProductPeriodName",
            "ProductWarehouseId",
            "ProductWarehouseName",
            "EmployeeId",
            "EmployeeName",
            "Quantity",
            "Price",
            "EngageRegionNames",
            "TransactionDate",
            "Note"];

        return new InventoryExcelReportVm<ProductTransactionDto>
        {
            Count = entity.Count,
            ReportName = "Inventory Report",
            Data = entity,
            ColumnNames = columns
        };
    }
}
