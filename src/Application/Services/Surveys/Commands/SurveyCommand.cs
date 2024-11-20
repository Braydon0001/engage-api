namespace Engage.Application.Services.Surveys.Commands;

public class SurveyCommand : IMapTo<Survey>
{
    public int SurveyTypeId { get; set; }
    public int EngageSubGroupId { get; set; }
    public int SupplierId { get; set; }
    public int EngageBrandId { get; set; }
    public int? EngageMasterProductId { get; set; }
    public string Title { get; set; }
    public string Note { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    //TODO remove
    public List<int> EngageRegions { get; set; }
    public List<int> Stores { get; set; }
    public bool IsRecurring { get; set; }
    //TODO Rename
    public bool IsEmployeeTargeting { get; set; }
    public bool IsRequired { get; set; }
    public bool IsDisabled { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyCommand, Survey>();
    }
}
