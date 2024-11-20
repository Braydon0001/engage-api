namespace Engage.Application.Services.Mobile.Orders.Models;

public class MobileOrderDto
{
    public int OrderId { get; set; }
    public string StoreName { get; set; }
    public string StoreImageUrl { get; set; }
    public string Status { get; set; }
    public string Reference { get; set; }
    public string OrderDate { get; set; }
    public string DeliveryDate { get; set; }
    public List<MobileOrderProductDto> Products { get; set; }
}

public class MobileOrderProductDto
{
    public int OrderSkuId { get; set; }
    public string ProductName { get; set; }
    public string ProductCode { get; set; }
    public int Quantity { get; set; }
    public string QuantityType { get; set; }
    public string UnitType { get; set; }
    public string Warehouse { get; set; }

   
}
