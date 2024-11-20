namespace Engage.Application.Services.ClaimReports.Models;

public class ClaimRelatedReportDto : IMapFrom<ClaimSku>
{
    public int StoreId { get; set; }
    public int ClaimId { get; set; }
    public string StoreName { get; set; }
    public string ClaimNumber { get; set; }
    public string ClaimReference { get; set; }
    public int Quantity { get; set; }
    public string CreatedDate { get; set; }
    public string ClaimDate { get; set; }
    public string FallsWithinPeriod { get; set; }
    public string ProductName { get; set; }
    public string ClaimSupplierName { get; set; }
    public string ProductSupplierName { get; set; }
    public int ProductSupplierId { get; set; }
    public string ClaimTypeName { get; set; }
    public string ClaimAccountManagerName { get; set; }
    public string ClaimManagerName { get; set; }
    public string IsPayStore { get; set; }
    public string IsClaimFromSupplier { get; set; }
    public decimal Amount { get; set; }
    public decimal VatAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal StoreClaimNumberTotal { get; set; }
    public decimal StoreTotal { get; set; }
    public string Note { get; set; }
    public string CreatedBy { get; set; }
    public DateTime ProductCreatedDate { get; set; }
    public string IsMaasProduct { get; set; }
    public string IsVAT { get; set; }
    public string Region { get; set; }
    public string Spar { get; set; }
    public string VatStatus { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ClaimSku, ClaimRelatedReportDto>()
            .ForMember(d => d.StoreId, opt => opt.MapFrom(s => s.Claim.StoreId))
            .ForMember(d => d.ClaimId, opt => opt.MapFrom(s => s.ClaimId))
            .ForMember(d => d.ClaimNumber, opt => opt.MapFrom(s => s.Claim.ClaimNumber))
            .ForMember(d => d.CreatedDate, opt => opt.MapFrom(s => s.Claim.Created.ToShortDateString()))
            .ForMember(d => d.ClaimDate, opt => opt.MapFrom(s => s.Claim.ClaimDate.ToShortDateString()))
            .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Claim.Store.Name))
            .ForMember(d => d.ClaimReference, opt => opt.MapFrom(s => s.Claim.ClaimReference))
            .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.DCProduct.Name))
            .ForMember(d => d.ClaimSupplierName, opt => opt.MapFrom(s => s.Claim.Supplier.Name))
            .ForMember(d => d.ProductSupplierName, opt => opt.MapFrom(s => s.DCProduct.EngageVariantProduct.EngageMasterProduct.IsAllSuppliersProduct == true ? s.Claim.Supplier.Name : s.DCProduct.EngageVariantProduct.EngageMasterProduct.Supplier.Name))
            .ForMember(d => d.ProductSupplierId, opt => opt.MapFrom(s => s.DCProduct.EngageVariantProduct.EngageMasterProduct.SupplierId))
            .ForMember(d => d.ClaimTypeName, opt => opt.MapFrom(s => s.Claim.ClaimType.Name))
            .ForMember(d => d.ClaimAccountManagerName, opt => opt.MapFrom(s => s.Claim.ClaimAccountManager.FullName))
            .ForMember(d => d.IsClaimFromSupplier, opt => opt.MapFrom(s => s.Claim.IsClaimFromSupplier ? "YES" : "NO"))
            .ForMember(d => d.IsPayStore, opt => opt.MapFrom(s => s.Claim.IsPayStore ? "YES" : "NO"))
            .ForMember(d => d.ClaimManagerName, opt => opt.MapFrom(s => s.Claim.ClaimManager.FullName))
            .ForMember(d => d.ProductCreatedDate, opt => opt.MapFrom(s => s.Created))
            .ForMember(d => d.IsMaasProduct, opt => opt.MapFrom(s => s.DCProduct.EngageVariantProduct.EngageMasterProduct.IsDairyProduct ? "YES" : "NO"))
            .ForMember(d => d.IsVAT, opt => opt.MapFrom(s => s.VatAmount > 0 ? "YES" : "NO"))
            .ForMember(d => d.Region, opt => opt.MapFrom(s => s.Claim.Store.EngageRegion.Name))
            .ForMember(d => d.VatStatus, opt => opt.MapFrom(s => s.VatAmount > 0 ? "Vatable" : "Non Vatable"));
    }
}
