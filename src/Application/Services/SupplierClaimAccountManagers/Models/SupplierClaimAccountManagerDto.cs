namespace Engage.Application.Services.SupplierClaimAccountManagers.Models;

public class SupplierClaimAccountManagerDto : IMapFrom<SupplierClaimAccountManager>
{
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public int ClaimAccountManagerId { get; set; }
    public string ClaimAccountManagerName { get; set; }
    public string Email { get; set; }
    public string MobilePhone { get; set; }


    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierClaimAccountManager, SupplierClaimAccountManagerDto>()
             .ForMember(d => d.SupplierName, opts => opts.MapFrom(s => s.Supplier.Name))
             .ForMember(d => d.ClaimAccountManagerId, opts => opts.MapFrom(s => s.User.UserId))
             .ForMember(d => d.ClaimAccountManagerName, opts => opts.MapFrom(s => s.User.FullName))
             .ForMember(d => d.Email, opts => opts.MapFrom(s => s.User.Email))
             .ForMember(d => d.MobilePhone, opts => opts.MapFrom(s => s.User.MobilePhone));
    }
}
