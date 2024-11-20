using Engage.Application.Services.SurveyFormSubmissions.Queries;

namespace Engage.Application.Services.SurveyFormAnswers.Queries;

public class SurveyFormAnswerPhotoDto : IMapFrom<SurveyFormAnswer>
{
    public int Id { get; init; }
    public DateTime? AnswerDate { get; init; }
    public List<JsonFile> Files { get; init; }
    public string StoreName { get; init; }
    public string StoreRegion { get; init; }
    public string EmployeeNameCode { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormAnswer, SurveyFormAnswerPhotoDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormAnswerId))
               .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.SurveyFormSubmission.Store.Name))
               .ForMember(d => d.StoreRegion, opt => opt.MapFrom(s => s.SurveyFormSubmission.Store.EngageRegion.Name))
               .ForMember(d => d.EmployeeNameCode, opt => opt.MapFrom(s => string.Join(" ", s.SurveyFormSubmission.Employee.FirstName, s.SurveyFormSubmission.Employee.LastName, $"({s.SurveyFormSubmission.Employee.Code})")));
    }
}
