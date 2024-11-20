namespace Engage.Application.Services.Vouchers.Commands;

public class VoucherCommand : IMapTo<Voucher>
{
    public int SupplierId { get; set; }
    public int EngageRegionId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal Total { get; set; }
    public string Note { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<VoucherCommand, Voucher>();
    }
}
