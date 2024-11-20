// auto-generated
namespace Engage.Application.Services.SupplierContractAmounts.Queries;

public class SupplierContractAmountDto : IMapFrom<SupplierContractAmount>
{
    public int Id { get; set; }
    public int SupplierSubContractDetailId { get; set; }
    public string SupplierSubContractDetailName { get; set; }
    public int SupplierContractAmountTypeId { get; set; }
    public string SupplierContractAmountTypeName { get; set; }
    public int SupplierContractSplitId { get; set; }
    public string SupplierContractSplitName { get; set; }
    public float Amount { get; set; }
    public float? StartRangeAmount { get; set; }
    public float? EndRangeAmount { get; set; }
    public bool IsAmountPercent { get; set; }
    public bool IsRangeAmountPercent { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierContractAmount, SupplierContractAmountDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SupplierContractAmountId));
    }
}
