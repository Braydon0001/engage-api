namespace Engage.Application.Services.SurveyFormSubmissions.Queries;

public class SurveyFormSubmissionOption : IMapFrom<SurveyFormSubmission>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormSubmission, SurveyFormSubmissionOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormSubmissionId));
    }
}