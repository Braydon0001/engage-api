// auto-generated
using Engage.Application.Services.SupplierPeriods.Queries;
using Engage.Application.Services.SupplierBudgetVersionTypes.Queries;

namespace Engage.Application.Services.SupplierBudgetVersions.Queries;

public class SupplierBudgetVersionVm : IMapFrom<SupplierBudgetVersion>
{
    public int Id { get; set; }
    public SupplierPeriodOption SupplierPeriodId { get; set; }
    public SupplierBudgetVersionTypeOption SupplierBudgetVersionTypeId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierBudgetVersion, SupplierBudgetVersionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierBudgetVersionId))
               .ForMember(d => d.SupplierPeriodId, opt => opt.MapFrom(s => s.SupplierPeriod))
               .ForMember(d => d.SupplierBudgetVersionTypeId, opt => opt.MapFrom(s => s.SupplierBudgetVersionType));
    }
}
