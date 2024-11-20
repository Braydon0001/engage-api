
namespace Engage.Application.Services.SurveyFormOptions.Queries;

public class SurveyFormOptionVm : IMapFrom<SurveyFormOption>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public bool CompleteSurvey { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormOption, SurveyFormOptionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormOptionId));
    }
}
