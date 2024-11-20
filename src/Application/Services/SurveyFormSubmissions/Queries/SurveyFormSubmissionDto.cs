namespace Engage.Application.Services.SurveyFormSubmissions.Queries;

public class SurveyFormSubmissionDto : IMapFrom<SurveyFormSubmission>
{
    public int Id { get; init; }
    public int? EmployeeId { get; init; }
    public int? UserId { get; init; }
    public int SurveyFormId { get; init; }
    public int? StoreId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormSubmission, SurveyFormSubmissionDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormSubmissionId));
    }
}
