namespace Engage.Application.Services.EmployeeStores.Models;

public class EmployeeStoreDto : IMapFrom<EmployeeStore>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string EmployeeName { get; set; }
    public int StoreId { get; set; }
    public string StoreName { get; set; }
    public int EngageSubGroupId { get; set; }
    public string EngageSubGroupName { get; set; }
    public int StoreDepartmentId { get; set; }
    public string StoreDepartmentName { get; set; }
    public int EngageDepartmentId { get; set; }
    public string EngageDepartmentName { get; set; }
    public int FrequencyTypeId { get; set; }
    public string FrequencyTypeName { get; set; }
    public int Frequency { get; set; }
    public bool Disabled { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStore, EmployeeStoreDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.EmployeeStoreId))
            .ForMember(d => d.EmployeeName, opt => opt.MapFrom(s => s.Employee.FirstName + " " + s.Employee.LastName))
            .ForMember(d => d.StoreDepartmentName, opt => opt.MapFrom(s => s.EngageSubGroup.StoreDepartment.Name))
            .ForMember(d => d.EngageDepartmentId, opt => opt.MapFrom(s => s.EngageSubGroup.EngageDepartment.Id))
            .ForMember(d => d.EngageDepartmentName, opt => opt.MapFrom(s => s.EngageSubGroup.EngageDepartment.Name));
    }
}
