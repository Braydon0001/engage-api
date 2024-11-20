namespace Engage.Application.Services.ClaimStatusUsers.Models
{
    public class ClaimStatusUserDto : IMapFrom<ClaimStatusUser>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ClaimStatusId { get; set; }
        public string ClaimStatusName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ClaimStatusUser, ClaimStatusUserDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ClaimStatusUserId))
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => $"{s.User.FirstName} {s.User.LastName}"))
                .ForMember(d => d.ClaimStatusName, opt => opt.MapFrom(s => s.ClaimStatus.Name));
        }        
    }
}
