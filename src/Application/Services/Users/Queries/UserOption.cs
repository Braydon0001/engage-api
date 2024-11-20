namespace Engage.Application.Services.Users.Queries;

public class UserOption : IMapFrom<User>
{
    public int Id { get; set; }
    public string FullName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<User, UserOption>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserId));
    }
}
