namespace Engage.Application.Services.SurveyFormOptions.Queries;

public class SurveyFormOptionDto : IMapFrom<SurveyFormOption>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public bool CompleteSurvey { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormOption, SurveyFormOptionDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormOptionId));
    }
}
