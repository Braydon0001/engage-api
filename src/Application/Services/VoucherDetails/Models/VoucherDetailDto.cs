namespace Engage.Application.Services.VoucherDetails.Models;

public class VoucherDetailDto : IMapFrom<VoucherDetail>
{
    public int Id { get; set; }
    public int VoucherId { get; set; }
    public string VoucherName { get; set; }
    public int VoucherDetailStatusId { get; set; }
    public string VoucherDetailStatusName { get; set; }
    public string VoucherNumber { get; set; }
    public decimal Amount { get; set; }
    public string Note { get; set; }
    public int? EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public int? StoreId { get; set; }
    public string StoreName { get; set; }
    public int? ClaimId { get; set; }
    public string ClaimNumber { get; set; }
    public DateTime PlacedDate { get; set; }
    public string PlacedBy { get; set; }
    public DateTime? AssignedDate { get; set; }
    public string AssignedBy { get; set; }
    public DateTime? ClosedDate { get; set; }
    public string ClosedBy { get; set; }
    public bool CanUpdate { get; set; }
    public List<JsonFile> Files { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<VoucherDetail, VoucherDetailDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.VoucherDetailId))
            .ForMember(d => d.ClaimNumber, opt => opt.MapFrom(s => s.ClaimId != null ? s.Claim.ClaimNumber : ""))
            .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.StoreId != null ? s.Store.Name : ""))
            .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.EmployeeId != null ? s.Employee.FirstName + " " + s.Employee.LastName + " - " + s.Employee.Code : ""))
            .ForMember(d => d.PlacedDate, opt => opt.MapFrom(s => s.Created))
            .ForMember(d => d.PlacedBy, opt => opt.MapFrom(s => s.CreatedBy))
            .ForMember(d => d.CanUpdate, opt => opt.MapFrom(s => s.VoucherDetailStatusId != (int)Domain.Enums.VoucherDetailStatusId.Issued));
    }
}
