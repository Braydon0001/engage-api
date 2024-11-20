namespace Engage.Application.Services.ClaimStatusUsers.Models
{
    public class ClaimStatusUserVm : IMapFrom<ClaimStatusUser>
    {
        public int Id { get; set; }
        public OptionDto UserId { get; set; }
        public OptionDto ClaimStatus { get; set; }
       
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ClaimStatusUser, ClaimStatusUserVm>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ClaimStatusUserId))
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => new OptionDto(s.UserId, $"{s.User.FirstName} {s.User.LastName}")))
                .ForMember(d => d.ClaimStatus, opt => opt.MapFrom(s => new OptionDto(s.ClaimStatus.Id, s.ClaimStatus.Name)));
        }

       
    }
}
