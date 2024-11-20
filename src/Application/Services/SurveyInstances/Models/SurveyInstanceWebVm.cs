namespace Engage.Application.Services.SurveyInstances.Models;

public class SurveyInstanceWebVm : IMapFrom<SurveyInstance>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public int SurveyId { get; set; }
    public string Note { get; set; }
    public DateTime SurveyDate { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyInstance, SurveyInstanceWebVm>()
             .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyInstanceId));
    }
}
