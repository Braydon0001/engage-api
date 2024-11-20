namespace Engage.Application.Services.VoucherDetails.Commands;

public class VoucherDetailCommand : IMapTo<VoucherDetail>
{
    public int VoucherId { get; set; }
    public string VoucherNumber { get; set; }
    public decimal Amount { get; set; }
    public string Note { get; set; }
    public int? ClaimId { get; set; }
    public int? StoreId { get; set; }
    public int? EmployeeId { get; set; }
    public int? StoreContactId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<VoucherDetailCommand, VoucherDetail>();
    }
}
