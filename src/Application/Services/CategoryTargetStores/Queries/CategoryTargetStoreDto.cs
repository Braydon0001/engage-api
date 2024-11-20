namespace Engage.Application.Services.CategoryTargetStores.Queries;

public class CategoryTargetStoreDto : IMapFrom<CategoryTargetStore>
{
    public int Id { get; init; }
    public int CategoryTargetId { get; init; }
    public int StoreId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryTargetStore, CategoryTargetStoreDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategoryTargetStoreId));
    }
}
