namespace Engage.Application.Services.OrderSkus.Models;

public class OrderSkuVM
{
    public OrderSkuDto OrderSku { get; internal set; }
    public ICollection<OptionDto> OrderSkuTypes { get; set; }
    public ICollection<OptionDto> OrderSkuStatuses { get; set; }
}