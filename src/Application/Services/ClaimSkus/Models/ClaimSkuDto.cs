namespace Engage.Application.Services.ClaimSkus.Models
{
    public class ClaimSkuDto : IMapFrom<ClaimSku>
    {
        public int Id { get; set; }
        public int ClaimId { get; set; }
        public int ClaimSkuTypeId { get; set; }
        public string ClaimSkuTypeName { get; set; }
        public int ClaimQuantityTypeId { get; set; }
        public string ClaimQuantityTypeName { get; set; }
        public int ClaimSkuStatusId { get; set; }
        public string ClaimSkuStatusName { get; set; }
        public decimal Amount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int Quantity { get; set; }
        public int DCProductId { get; set; }
        public string DCProductCode { get; set; }
        public string DCProductName { get; set; }
        public float DCProductSize { get; set; }
        public float DCProductPackSize { get; set; }
        public string DCProductUnitType { get; set; }
        public int EngageVariantProductId { get; set; }
        public string EngageVariantProductCode { get; set; }
        public string EngageVariantProductName { get; set; }
        public int VatId { get; set; }
        public int ClaimVatId { get; set; }
        public string VatCode { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public string Note { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ClaimSku, ClaimSkuDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ClaimSkuId))
                .ForMember(d => d.ClaimSkuTypeName, opt => opt.MapFrom(s => s.ClaimSkuType.Name))
                .ForMember(d => d.ClaimQuantityTypeName, opt => opt.MapFrom(s => s.ClaimQuantityType.Name))
                .ForMember(d => d.ClaimSkuStatusName, opt => opt.MapFrom(s => s.ClaimSkuStatus.Name))
                .ForMember(d => d.DCProductCode, opt => opt.MapFrom(s => s.DCProduct.Code))
                .ForMember(d => d.DCProductName, opt => opt.MapFrom(s => s.DCProduct.Name))
                .ForMember(d => d.DCProductSize, opt => opt.MapFrom(s => s.DCProduct.Size))
                .ForMember(d => d.DCProductPackSize, opt => opt.MapFrom(s => s.DCProduct.PackSize))
                .ForMember(d => d.DCProductUnitType, opt => opt.MapFrom(s => s.DCProduct.UnitType.Name))
                .ForMember(d => d.EngageVariantProductId, opt => opt.MapFrom(s => s.DCProduct.EngageVariantProductId))
                .ForMember(d => d.EngageVariantProductCode, opt => opt.MapFrom(s => s.DCProduct.EngageVariantProduct.Code))
                .ForMember(d => d.EngageVariantProductName, opt => opt.MapFrom(s => s.DCProduct.EngageVariantProduct.Name))
                .ForMember(d => d.VatId, opt => opt.MapFrom(s => s.DCProduct.EngageVariantProduct.EngageMasterProduct.VatId))
                .ForMember(d => d.VatCode, opt => opt.MapFrom(s => s.DCProduct.EngageVariantProduct.EngageMasterProduct.Vat.Code))
                .ForMember(d => d.ClaimVatId, opt => opt.MapFrom(s => s.Claim.VatId));
        }

    }
}
