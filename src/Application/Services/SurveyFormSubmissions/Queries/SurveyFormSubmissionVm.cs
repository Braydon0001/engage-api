
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.Stores.Queries;
using Engage.Application.Services.Users.Queries;

namespace Engage.Application.Services.SurveyFormSubmissions.Queries;

public class SurveyFormSubmissionVm : IMapFrom<SurveyFormSubmission>
{
    public int Id { get; init; }
    public EmployeeOption EmployeeId { get; init; }
    public UserOption UserId { get; init; }
    public int SurveyFormId { get; init; }
    public StoreOption StoreId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyFormSubmission, SurveyFormSubmissionVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyFormSubmissionId))
               .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.Employee))
               .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.User))
               .ForMember(d => d.StoreId, opt => opt.MapFrom(s => s.Store));
    }
}
