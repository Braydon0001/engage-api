namespace Engage.Application.Services.Surveys.Models;

public class SurveyListDto : IMapFrom<Survey>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string SurveyDate { get; set; }
    public string SurveyTypeName { get; set; }
    public string EngageSubGroupName { get; set; }
    public string SupplierName { get; set; }
    public string EngageBrandName { get; set; }
    public bool IsRecurring { get; set; }
    public bool IsEmployeeTargeting { get; set; }
    public bool IsRequired { get; set; }
    public bool Disabled { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Survey, SurveyListDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyId))
            .ForMember(d => d.SurveyDate, opt => opt.MapFrom(s => DateUtils.ShortDateString(s.StartDate, s.EndDate)))
            .ForMember(d => d.SurveyTypeName, opt => opt.MapFrom(s => s.SurveyType.Name))
            .ForMember(d => d.EngageSubGroupName, opt => opt.MapFrom(s => s.EngageSubGroup.Name))
            .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Supplier.Name))
            .ForMember(d => d.EngageBrandName, opt => opt.MapFrom(s => s.EngageBrand.Name));
    }
}
