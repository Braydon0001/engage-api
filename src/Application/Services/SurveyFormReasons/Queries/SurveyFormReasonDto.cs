namespace Engage.Application.Services.SurveyFormReasons.Queries;

public class SurveyFormReasonDto : IMapFrom<SurveyFormReason>
{
    public int Id { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormReason, SurveyFormReasonDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormReasonId));
    }
}
