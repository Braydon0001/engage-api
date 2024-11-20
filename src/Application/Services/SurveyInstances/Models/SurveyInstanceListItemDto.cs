namespace Engage.Application.Services.SurveyInstances.Models;

public class SurveyInstanceListItemDto : IMapFrom<SurveyInstance>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public int SurveyId { get; set; }
    public string SurveyTitle { get; set; }
    public string Note { get; set; }
    public DateTime SurveyDate { get; set; }
    public int EngageSubGroupId { get; set; }
    public string EngageSubGroupName { get; set; }
    public int SupplierId { get; set; }
    public string SupplierName { get; set; }
    public int EngageBrandId { get; set; }
    public string EngageBrandName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SurveyInstance, SurveyInstanceListItemDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.SurveyInstanceId))
            .ForMember(d => d.StoreName, opt => opt.MapFrom(s => s.Store.Name))
            .ForMember(d => d.EngageSubGroupId, opt => opt.MapFrom(s => s.Survey.EngageSubGroupId))
            .ForMember(d => d.EngageSubGroupName, opt => opt.MapFrom(s => s.Survey.EngageSubGroup.Name))
            .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.Survey.SupplierId))
            .ForMember(d => d.SupplierName, opt => opt.MapFrom(s => s.Survey.Supplier.Name))
            .ForMember(d => d.EngageBrandId, opt => opt.MapFrom(s => s.Survey.EngageBrandId))
            .ForMember(d => d.EngageBrandName, opt => opt.MapFrom(s => s.Survey.EngageBrand.Name));
    }
}
