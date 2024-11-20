namespace Engage.Application.Services.SurveyFormReasons.Queries;

public class SurveyFormReasonOption : IMapFrom<SurveyFormReason>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormReason, SurveyFormReasonOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormReasonId));
    }
}