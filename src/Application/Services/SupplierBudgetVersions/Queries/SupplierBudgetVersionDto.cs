// auto-generated
namespace Engage.Application.Services.SupplierBudgetVersions.Queries;

public class SupplierBudgetVersionDto : IMapFrom<SupplierBudgetVersion>
{
    public int Id { get; set; }
    public int SupplierPeriodId { get; set; }
    public string SupplierPeriodName { get; set; }
    public int SupplierBudgetVersionTypeId { get; set; }
    public string SupplierBudgetVersionTypeName { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierBudgetVersion, SupplierBudgetVersionDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierBudgetVersionId));
    }
}
