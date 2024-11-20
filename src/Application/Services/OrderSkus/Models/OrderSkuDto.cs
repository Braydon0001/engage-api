namespace Engage.Application.Services.OrderSkus.Models
{
    public class OrderSkuDto : IMapFrom<OrderSku>
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int OrderSkuTypeId { get; set; }
        public int OrderSkuStatusId { get; set; }
        public int DCProductId { get; set; }
        public int OrderQuantityTypeId { get; set; }
        public int Quantity { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Note { get; set; }
        public List<JsonFile> Files { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<OrderSku, OrderSkuDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(d => d.OrderSkuId));
        }
    }
}
