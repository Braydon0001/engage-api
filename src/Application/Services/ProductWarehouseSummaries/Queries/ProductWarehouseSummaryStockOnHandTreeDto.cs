namespace Engage.Application.Services.ProductWarehouseSummaries.Queries;

public class ProductWarehouseSummaryStockOnHandTreeDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int ProductWarehouseId { get; set; }
    public float CurrentStock { get; set; }
    public string ProductWarehouseName { get; set; }
    public Dictionary<int, float> Quantities { get; set; }
    public bool IsParent { get; set; }
}