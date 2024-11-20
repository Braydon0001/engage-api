namespace Engage.Application.Services.EngageSubGroups.Models;

public class EngageSubGroupVm : IMapFrom<EngageSubGroup>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int? Order { get; set; }
    public OptionDto EngageGroupId { get; set; }
    public OptionDto StoreDepartmentId { get; set; }
    public OptionDto EngageDepartmentId { get; set; }
    public List<OptionDto> SupplierIds { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EngageSubGroup, EngageSubGroupVm>()
            .ForMember(d => d.EngageGroupId, opt => opt.MapFrom(s => new OptionDto(s.EngageGroupId, s.EngageGroup.Name)))
            .ForMember(d => d.StoreDepartmentId, opt => opt.MapFrom(s => new OptionDto(s.StoreDepartmentId, s.StoreDepartment.Name)))
            .ForMember(d => d.EngageDepartmentId, opt => opt.MapFrom(s => new OptionDto(s.EngageDepartmentId, s.EngageDepartment.Name)))
            .ForMember(d => d.SupplierIds, opt => opt.MapFrom(s => s.EngageSubGroupSuppliers.Select(o => new OptionDto(o.SupplierId, o.Supplier.Name))));
    }
}
