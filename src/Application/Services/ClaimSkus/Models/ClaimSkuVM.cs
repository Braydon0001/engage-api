namespace Engage.Application.Services.ClaimSkus.Models
{
    public class ClaimSkuVm : IMapFrom<ClaimSku>
    {
        public int Id { get; set; }
        public int ClaimId { get; set; }
        public OptionDto ClaimSkuTypeId { get; set; }
        public OptionDto ClaimQuantityTypeId { get; set; }
        public OptionDto ClaimSkuStatusId { get; set; }
        public decimal Amount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int Quantity { get; set; }
        public OptionDto DCProductId { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public string Note { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ClaimSku, ClaimSkuVm>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ClaimSkuId))
                .ForMember(d => d.ClaimSkuTypeId, opt => opt.MapFrom(s => new OptionDto(s.ClaimSkuTypeId, s.ClaimSkuType.Name)))
                .ForMember(d => d.ClaimQuantityTypeId, opt => opt.MapFrom(s => new OptionDto(s.ClaimQuantityTypeId, s.ClaimQuantityType.Name)))
                .ForMember(d => d.ClaimSkuStatusId, opt => opt.MapFrom(s => new OptionDto(s.ClaimSkuStatus.Id, s.ClaimSkuStatus.Name)))                
                .ForMember(d => d.DCProductId, opt => opt.MapFrom(s => new OptionDto(s.DCProductId, s.DCProduct.Code + "" + s.DCProduct.Name)));
        }
    }
}