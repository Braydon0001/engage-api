namespace Engage.Application.Services.EngageMasterProducts.Models;

public class ProductListDto
{
    public string Type { get; set; }
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public bool Disabled { get; set; }
    public bool Deleted { get; set; }
    public string ProductActiveStatusName { get; set; }
    public string ProductStatusName { get; set; }
    public string ProductWarehouseStatusName { get; set; }
    public string ProductSubWarehouseName { get; set; }
    public List<JsonFile> Files { get; set; }
}
