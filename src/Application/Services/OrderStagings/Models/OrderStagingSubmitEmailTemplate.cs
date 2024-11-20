namespace Engage.Application.Services.OrderStagings.Models;

internal class OrderStagingSubmitEmailTemplate
{
    [JsonProperty("orderCreator")]
    public string Name { get; set; }
    [JsonProperty("storeName")]
    public string StoreName { get; set; }
    [JsonProperty("orderDate")]
    public string OrderDate { get; set; }
    public int OrderStagingId { get; set; }
}
