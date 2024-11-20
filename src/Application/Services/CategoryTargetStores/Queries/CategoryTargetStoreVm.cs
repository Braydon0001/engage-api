using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.CategoryTargetStores.Queries;

public class CategoryTargetStoreVm : IMapFrom<CategoryTargetStore>
{
    public int Id { get; init; }
    //public CategoryTargetOption CategoryTargetId { get; init; }
    public StoreOption StoreId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryTargetStore, CategoryTargetStoreVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategoryTargetStoreId))
               //.ForMember(d => d.CategoryTargetId, opt => opt.MapFrom(s => s.CategoryTarget))
               .ForMember(d => d.StoreId, opt => opt.MapFrom(s => s.Store));
    }
}
