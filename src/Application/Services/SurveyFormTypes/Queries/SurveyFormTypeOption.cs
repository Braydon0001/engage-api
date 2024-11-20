namespace Engage.Application.Services.SurveyFormTypes.Queries;

public class SurveyFormTypeOption : IMapFrom<SurveyFormType>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormType, SurveyFormTypeOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormTypeId));
    }
}