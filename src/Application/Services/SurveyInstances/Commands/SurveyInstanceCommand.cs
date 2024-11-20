namespace Engage.Application.Services.SurveyInstances.Commands;

public class SurveyInstanceCommand : IMapTo<SurveyInstance>
{
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public int SurveyId { get; set; }
    public string Note { get; set; }
    public DateTime SurveyDate { get; set; }

    public void Mapping(AutoMapper.Profile profile)
    {
        profile.CreateMap<SurveyInstanceCommand, SurveyInstance>();
    }
}
