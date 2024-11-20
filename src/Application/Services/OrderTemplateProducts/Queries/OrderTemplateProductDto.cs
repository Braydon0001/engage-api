namespace Engage.Application.Services.OrderTemplateProducts.Queries;

public class OrderTemplateProductDto : IMapFrom<OrderTemplateProduct>
{
    public int Id { get; set; }
    public int OrderTemplateGroupId { get; set; }
    public string OrderTemplateGroupName { get; set; }
    public int DcProductId { get; set; }
    public string DcProductName { get; set; }
    public int Order { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal PromotionPrice { get; set; }
    public decimal RecommendedPrice { get; set; }
    public decimal GrossProfitPercent { get; set; }
    public string Suffix { get; set; }
    public string Note { get; set; }
    public List<JsonFile> Files { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderTemplateProduct, OrderTemplateProductDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderTemplateProductId));
    }
}
