namespace Engage.Application.Services.ApiKeys.Queries;

public class ApiKeyDto : IMapFrom<ApiKey>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Value { get; init; }
    public string AssignedTo { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ApiKey, ApiKeyDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.ApiKeyId));
    }
}
