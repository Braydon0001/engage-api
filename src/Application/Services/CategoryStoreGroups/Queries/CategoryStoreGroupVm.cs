
using Engage.Application.Services.CategoryGroups.Queries;
using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.CategoryStoreGroups.Queries;

public class CategoryStoreGroupVm : IMapFrom<CategoryStoreGroup>
{
    public int Id { get; init; }
    public CategoryGroupOption CategoryGroupId { get; init; }
    public StoreOption StoreId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryStoreGroup, CategoryStoreGroupVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategoryStoreGroupId))
               .ForMember(d => d.CategoryGroupId, opt => opt.MapFrom(s => s.CategoryGroup))
               .ForMember(d => d.StoreId, opt => opt.MapFrom(s => s.Store));
    }
}
