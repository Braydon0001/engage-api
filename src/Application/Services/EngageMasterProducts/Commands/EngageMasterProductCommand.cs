namespace Engage.Application.Services.EngageMasterProducts.Commands;

public class EngageMasterProductCommand : IMapTo<EngageMasterProduct>
{
    public int SupplierId { get; set; }
    public int ProductClassificationId { get; set; }
    public int EngageDepartmentId { get; set; }
    public int EngageSubCategoryId { get; set; }
    public int EngageBrandId { get; set; }
    public int VatId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public bool IsVATProduct { get; set; }
    public bool IsDairyProduct { get; set; }
    public bool Disabled { get; set; }
    public bool IsAllSuppliersProduct { get; set; }
    public bool IsFreshProduct { get; set; }
    public bool IsDropShipment { get; set; }

    public List<int> EngageTagIds { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageMasterProductCommand, EngageMasterProduct>();
    }
}
