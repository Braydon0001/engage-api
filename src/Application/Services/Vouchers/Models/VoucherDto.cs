namespace Engage.Application.Services.Vouchers.Models;

public class VoucherDto : IMapFrom<Voucher>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Total { get; set; }
    public string Note { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public int EngageRegionId { get; set; }
    public string EngageRegionName { get; set; }
    public int VoucherStatusId { get; set; }
    public string VoucherStatusName { get; set; }
    public DateTime? ClosedDate { get; set; }
    public string ClosedBy { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public bool CanUpdate { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<Voucher, VoucherDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.VoucherId));
    }
}
public class VoucherSubTotalDto : VoucherDto, IMapFrom<Voucher>
{
    public decimal UsedTotal { get; set; }
    public decimal RemainingTotal { get; set; }
    public new void Mapping(Profile profile)
    {
        profile.CreateMap<Voucher, VoucherSubTotalDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.VoucherId))
            .ForMember(d => d.UsedTotal, opt => opt.MapFrom(s => s.VoucherDetails.Where(e => e.Deleted == false).Sum(e => e.Amount)))
            .ForMember(d => d.RemainingTotal, opt => opt.MapFrom(s => s.Total - s.VoucherDetails.Where(e => e.Deleted == false).Sum(e => e.Amount)))
            .ForMember(d => d.CanUpdate, opt => opt.MapFrom(s => s.VoucherStatusId != (int)Domain.Enums.VoucherStatusId.Closed));
    }
}
