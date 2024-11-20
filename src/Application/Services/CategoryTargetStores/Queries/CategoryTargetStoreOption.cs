namespace Engage.Application.Services.CategoryTargetStores.Queries;

public class CategoryTargetStoreOption : IMapFrom<CategoryTargetStore>
{
    public int Id { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryTargetStore, CategoryTargetStoreOption>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategoryTargetStoreId));
    }
}