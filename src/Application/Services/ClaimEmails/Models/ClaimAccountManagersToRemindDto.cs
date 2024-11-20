namespace Engage.Application.Services.ClaimEmails.Models;
    public class ClaimAccountManagersToRemindDto : IMapFrom<SupplierClaimAccountManager>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string SupplierName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SupplierClaimAccountManager, ClaimAccountManagersToRemindDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserId))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.User.FirstName))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.User.LastName))
            .ForMember(d => d.Email, opt => opt.MapFrom(s => s.User.Email))
            .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Supplier.Name));
    }
}
