namespace Engage.Application.Services.EmployeeStores.Models;

public class EmployeeStoreVm : IMapFrom<EmployeeStore>
{
    public int Id { get; set; }
    public OptionDto EmployeeId { get; set; }
    public OptionDto StoreId { get; set; }
    public OptionDto EngageDepartmentId { get; set; }
    public OptionDto EngageSubGroupId { get; set; }
    public OptionDto FrequencyTypeId { get; set; }
    public int Frequency { get; set; }
    public string AccountNumber { get; set; }
    public string Note { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeStore, EmployeeStoreVm>()
            .ForMember(d => d.Id, opts => opts.MapFrom(s => s.EmployeeStoreId))
            .ForMember(d => d.EmployeeId, opts => opts.MapFrom(s => new OptionDto { Id = s.EmployeeId, Name = $"{s.Employee.FirstName} {s.Employee.LastName}" }))
            .ForMember(d => d.StoreId, opts => opts.MapFrom(s => new OptionDto { Id = s.StoreId, Name = s.Store.Name }))
            .ForMember(d => d.EngageDepartmentId, opts => opts.MapFrom(s => new OptionDto { Id = s.EngageSubGroup.EngageDepartmentId, Name = s.EngageSubGroup.EngageDepartment.Name }))
            .ForMember(d => d.EngageSubGroupId, opts => opts.MapFrom(s => new OptionDto { Id = s.EngageSubGroupId, Name = s.EngageSubGroup.Name }))
            .ForMember(d => d.FrequencyTypeId, opts => opts.MapFrom(s => new OptionDto { Id = s.FrequencyTypeId, Name = s.GetFrequencyType.Name }));
    }
}
