namespace Engage.Application.Services.UserEntities.Models;

public class UserEntityDto : IMapFrom<UserEntity>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserFullName { get; set; }
    public string Entity { get; set; }
    public int EntityIds { get; set; }
    public bool Deny { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserEntity, UserEntityDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.UserEntityId))
            .ForMember(d => d.EntityIds, opt => opt.MapFrom(s => s.UserEntityRecords.Select(e => e.UserEntityId)));
    }
}
