namespace Engage.Application.Services.Vouchers.Models;

public class VoucherVm : IMapFrom<Voucher>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Total { get; set; }
    public string Note { get; set; }
    public OptionDto VoucherTypeId { get; set; }
    public OptionDto SupplierId { get; set; }
    public OptionDto VoucherStatusId { get; set; }
    public OptionDto EngageRegionId { get; set; }
    public DateTime? ClosedDate { get; set; }
    public string ClosedBy { get; set; }
    public DateTime Created { get; set; }
    public string CreatedBy { get; set; }
    public bool CanUpdate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Voucher, VoucherVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.VoucherId))
            .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => new OptionDto(s.SupplierId, s.Supplier.Name)))
            .ForMember(d => d.VoucherStatusId, opt => opt.MapFrom(s => new OptionDto(s.VoucherStatus.Id, s.VoucherStatus.Name)))
            .ForMember(d => d.EngageRegionId, opt => opt.MapFrom(s => new OptionDto(s.EngageRegion.Id, s.EngageRegion.Name)))
            .ForMember(d => d.CanUpdate, opt => opt.MapFrom(s => s.VoucherStatusId != (int)Domain.Enums.VoucherStatusId.Closed));
    }
}
