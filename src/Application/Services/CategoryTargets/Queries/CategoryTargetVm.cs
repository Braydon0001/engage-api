using Engage.Application.Services.Suppliers.Queries;

namespace Engage.Application.Services.CategoryTargets.Queries;

public class CategoryTargetVm : IMapFrom<CategoryTarget>
{
    public int Id { get; init; }
    public SupplierOption SupplierId { get; init; }
    public float? Target { get; init; }
    public string AvailableLabel { get; init; }
    public string OccupiedLabel { get; init; }
    public string TextQuestion { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryTarget, CategoryTargetVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategoryTargetId))
               .ForMember(d => d.SupplierId, opt => opt.MapFrom(s => s.Supplier));
    }
}
