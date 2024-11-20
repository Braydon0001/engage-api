namespace Engage.Application.Services.VoucherDetails.Models;

public class VoucherDetailVm : IMapFrom<VoucherDetail>
{
    public int Id { get; set; }
    public int VoucherId { get; set; }
    public string VoucherNumber { get; set; }
    public decimal Amount { get; set; }
    public string Note { get; set; }
    public DateTime? AssignedDate { get; set; }
    public string AssignedBy { get; set; }
    public DateTime? ClosedDate { get; set; }
    public string ClosedBy { get; set; }

    public OptionDto EmployeeId { get; set; }
    public OptionDto StoreId { get; set; }
    public OptionDto ClaimId { get; set; }
    public bool CanUpdate { get; set; }
    public List<JsonFile> Files { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<VoucherDetail, VoucherDetailVm>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.VoucherDetailId))
            .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.EmployeeId.HasValue
                                                                 ? new OptionDto(s.EmployeeId.Value, $"{s.Employee.FirstName} {s.Employee.LastName} - {s.Employee.Code}")
                                                                 : null))
            .ForMember(d => d.StoreId, opt => opt.MapFrom(s => s.ClaimId.HasValue
                                                                               ? new OptionDto(s.Claim.StoreId, s.Claim.Store.Name)
                                                                               : null))
            .ForMember(d => d.ClaimId, opt => opt.MapFrom(s => s.ClaimId.HasValue
                                                                          ? new OptionDto(s.ClaimId.Value, $"{s.Claim.ClaimNumber} - {s.Claim.Store.Name}")
                                                                          : null))
            .ForMember(d => d.CanUpdate, opt => opt.MapFrom(s => s.VoucherDetailStatusId != (int)VoucherDetailStatusId.Issued));
    }
}
