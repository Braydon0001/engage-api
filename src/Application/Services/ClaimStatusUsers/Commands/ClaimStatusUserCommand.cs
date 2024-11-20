namespace Engage.Application.Services.ClaimStatusUsers.Commands
{
    public class ClaimStatusUserCommand : IMapTo<ClaimStatusUser>
    {
        public int UserId { get; set; }
        public int ClaimStatus { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ClaimStatusUser, ClaimStatusUserCommand>();
        }
    }
}
