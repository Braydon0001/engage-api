namespace Engage.Application.Services.Surveys.Models;

public class SurveyTargetingVM
{
    public SurveyDto Survey { get; set; }
    public ICollection<OptionDto> UnassignedEngageRegions { get; set; }
}
