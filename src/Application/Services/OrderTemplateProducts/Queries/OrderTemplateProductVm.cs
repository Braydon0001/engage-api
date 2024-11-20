using Engage.Application.Services.DCProducts.Models;
using Engage.Application.Services.OrderTemplateGroups.Queries;
using Engage.Application.Services.OrderTemplates.Queries;

namespace Engage.Application.Services.OrderTemplateProducts.Queries;

public class OrderTemplateProductVm : IMapFrom<OrderTemplateProduct>
{
    public int Id { get; set; }
    public OrderTemplateOption OrderTemplateId { get; set; }
    public OrderTemplateGroupOption OrderTemplateGroupId { get; set; }
    public DCProductVm DcProductId { get; set; }
    public int Order { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal PromotionPrice { get; set; }
    public decimal RecommendedPrice { get; set; }
    public decimal GrossProfitPercent { get; set; }
    public string Suffix { get; set; }
    public string Note { get; set; }
    public List<JsonFile> Files { get; set; }
    public List<JsonFile> EngageVariantProductFiles { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OrderTemplateProduct, OrderTemplateProductVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OrderTemplateProductId))
               .ForMember(d => d.OrderTemplateGroupId, opt => opt.MapFrom(s => s.OrderTemplateGroup))
               .ForMember(d => d.DcProductId, opt => opt.MapFrom(s => s.DCProduct))
               .ForMember(d => d.EngageVariantProductFiles, opt => opt.MapFrom(s => s.DCProduct.EngageVariantProduct.Files));
    }
}
