namespace Engage.Application.Services.CategoryTargets.Queries;

public class CategoryTargetDto : IMapFrom<CategoryTarget>
{
    public int Id { get; init; }
    public string StoreName { get; init; }
    public int SupplierId { get; init; }
    public string SupplierName { get; init; }
    public int? CategoryTargetTypeId { get; set; }
    public string CategoryTargetTypeName { get; init; }
    public float? Target { get; init; }
    public string AvailableLabel { get; init; }
    public string OccupiedLabel { get; init; }
    public string TextQuestion { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryTarget, CategoryTargetDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategoryTargetId))
               .ForMember(d => d.CategoryTargetTypeId, opt => opt.MapFrom(s => s.CategoryTargetTypeId))
               .ForMember(d => d.CategoryTargetTypeName, opt => opt.MapFrom(s => s.CategoryTargetType.Name));


    }
}
