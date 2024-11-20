namespace Engage.Application.Services.Orders.Models;

public class OrderSubmitEmailTemplate
{
    [JsonProperty("orderCreator")]
    public string Name { get; set; }
    [JsonProperty("storeName")]
    public string StoreName { get; set; }
    [JsonProperty("orderDate")]
    public string OrderDate { get; set; }
    public int OrderId { get; set; }
}
