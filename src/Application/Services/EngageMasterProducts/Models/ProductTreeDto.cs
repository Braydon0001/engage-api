namespace Engage.Application.Services.EngageMasterProducts.Models;

public class ProductTreeDto
{
    public int Id { get; set; }
    public string Type { get; set; }
    public bool IsParent { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public bool Disabled { get; set; }
    public bool Deleted { get; set; }
    public string EngageSubCategory { get; set; }
    public string ProductActiveStatus { get; set; }
    public string ProductStatus { get; set; }
    public string ProductWarehouseStatus { get; set; }
    public string ProductSubWarehouse { get; set; }

}
