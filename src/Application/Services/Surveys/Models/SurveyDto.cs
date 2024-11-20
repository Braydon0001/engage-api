namespace Engage.Application.Services.Surveys.Models;

public class SurveyDto : IMapFrom<Survey>
{
    public int Id { get; set; }
    public int SurveyTypeId { get; set; }
    public int EngageSubGroupId { get; set; }
    public string EngageSubGroupName { get; set; }
    public int SupplierId { get; set; }
    public int EngageBrandId { get; set; }
    public string Title { get; set; }
    public string Note { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsRecurring { get; set; }
    public bool IsEmployeeTargeting { get; set; }
    public bool IsRequired { get; set; }
    public bool Disabled { get; set; }
    public ICollection<OptionDto> EngageRegions { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Survey, SurveyDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyId))
            .ForMember(d => d.EngageSubGroupName, opt => opt.MapFrom(s => s.EngageSubGroup.Name))
            .ForMember(d => d.EngageRegions, opt => opt.MapFrom(s => s.SurveyEngageRegions
                                                                         .Select(s => s.EngageRegion)
                                                                         .Select(s => new OptionDto() { Id = s.Id, Name = s.Name })));
    }
}
