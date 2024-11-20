namespace Engage.Application.Services.SurveyFormOptions.Queries;

public class SurveyFormOptionOption : IMapFrom<SurveyFormOption>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormOption, SurveyFormOptionOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormOptionId));
    }
}