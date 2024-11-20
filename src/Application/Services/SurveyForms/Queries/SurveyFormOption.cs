namespace Engage.Application.Services.SurveyForms.Queries;

public class SurveyFormOption : IMapFrom<SurveyForm>
{
    public int Id { get; init; }

    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyForm, SurveyFormOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormId))
               .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Title));
    }
}