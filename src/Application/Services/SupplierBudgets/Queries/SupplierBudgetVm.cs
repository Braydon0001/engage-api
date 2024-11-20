// auto-generated
using Engage.Application.Services.SupplierBudgetTypes.Queries;
using Engage.Application.Services.SupplierBudgetVersions.Queries;
using Engage.Application.Services.SupplierContractDetails.Queries;

namespace Engage.Application.Services.SupplierBudgets.Queries;

public class SupplierBudgetVm : IMapFrom<SupplierBudget>
{
    public int Id { get; set; }
    public SupplierBudgetVersionOption SupplierBudgetVersionId { get; set; }
    public SupplierBudgetTypeOption SupplierBudgetTypeId { get; set; }
    public OptionDto SupplierId { get; set; }
    public OptionDto SupplierContract { get; set; }
    public SupplierContractDetailOption SupplierContractDetailId { get; set; }
    public OptionDto EngageRegionId { get; set; }
    public float Amount { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierBudget, SupplierBudgetVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierBudgetId))
               .ForMember(d => d.SupplierBudgetVersionId, opt => opt.MapFrom(s => s.SupplierBudgetVersion))
               .ForMember(d => d.SupplierBudgetTypeId, opt => opt.MapFrom(s => s.SupplierBudgetType))
               .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => new OptionDto(s.Supplier.SupplierId, s.Supplier.Name)))
               .ForMember(d => d.SupplierContract, opt => opt.MapFrom(s =>
                                new OptionDto
                                    (s.SupplierContractDetail.SupplierContractId,
                                    s.SupplierContractDetail.SupplierContract.Name
                                )))
               .ForMember(d => d.SupplierContractDetailId, opt => opt.MapFrom(s => s.SupplierContractDetail))
               .ForMember(d => d.EngageRegionId, opt => opt.MapFrom(s => new OptionDto(s.EngageRegion.Id, s.EngageRegion.Name)));
    }
}
