// auto-generated
using Engage.Application.Services.SupplierContractAmountTypes.Queries;
using Engage.Application.Services.SupplierContractSplits.Queries;
using Engage.Application.Services.SupplierSubContractDetails.Queries;

namespace Engage.Application.Services.SupplierContractAmounts.Queries;

public class SupplierContractAmountVm : IMapFrom<SupplierContractAmount>
{
    public int Id { get; set; }
    public SupplierSubContractDetailOption SupplierSubContractDetailId { get; set; }
    public SupplierContractAmountTypeOption SupplierContractAmountTypeId { get; set; }
    public SupplierContractSplitOption SupplierContractSplitId { get; set; }
    public float Amount { get; set; }
    public float? StartRangeAmount { get; set; }
    public float? EndRangeAmount { get; set; }
    public bool IsAmountPercent { get; set; }
    public bool IsRangeAmountPercent { get; set; }
    public List<JsonText> Note { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractAmount, SupplierContractAmountVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierContractAmountId))
               .ForMember(d => d.SupplierSubContractDetailId, opt => opt.MapFrom(s => s.SupplierSubContractDetail))
               .ForMember(d => d.SupplierContractAmountTypeId, opt => opt.MapFrom(s => s.SupplierContractAmountType))
               .ForMember(d => d.SupplierContractSplitId, opt => opt.MapFrom(s => s.SupplierContractSplit));
    }
}
