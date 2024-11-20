// auto-generated
namespace Engage.Application.Services.SupplierBudgets.Queries;

public class SupplierBudgetDto : IMapFrom<SupplierBudget>
{
    public int Id { get; set; }
    public int SupplierBudgetVersionId { get; set; }
    public string SupplierBudgetVersionName { get; set; }
    public int SupplierBudgetTypeId { get; set; }
    public string SupplierBudgetTypeName { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public int SupplierContractDetailId { get; set; }
    public string SupplierContractDetailName { get; set; }
    public int EngageRegionId { get; set; }
    public string EngageRegionName { get; set; }
    public float Amount { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierBudget, SupplierBudgetDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierBudgetId))
               .ForMember(d => d.SupplierBudgetVersionName, opt => opt.MapFrom
                                (s =>
                                s.SupplierBudgetVersion.SupplierPeriod.Name + " - " + s.SupplierBudgetVersion.SupplierBudgetVersionType.Name
                ));
    }
}
