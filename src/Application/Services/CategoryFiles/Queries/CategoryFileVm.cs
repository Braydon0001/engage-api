
using Engage.Application.Services.CategoryFileTypes.Queries;
using Engage.Application.Services.CategoryGroups.Queries;
using Engage.Application.Services.CategorySubGroups.Queries;
using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.CategoryFiles.Queries;

public class CategoryFileVm : IMapFrom<CategoryFile>
{
    public int Id { get; init; }
    public CategoryFileTypeOption CategoryFileTypeId { get; init; }
    public string Name { get; init; }
    public string Note { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public List<JsonFile> Files { get; init; }
    public StoreOption StoreId { get; init; }
    public CategoryGroupOption CategoryGroupId { get; init; }
    public CategorySubGroupOption CategorySubGroupId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryFile, CategoryFileVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategoryFileId))
               .ForMember(d => d.CategoryFileTypeId, opt => opt.MapFrom(s => s.CategoryFileType))
               .ForMember(d => d.StoreId, opt => opt.MapFrom(s => s.Store))
               .ForMember(d => d.CategoryGroupId, opt => opt.MapFrom(s => s.CategoryGroup))
               .ForMember(d => d.CategorySubGroupId, opt => opt.MapFrom(s => s.CategorySubGroup));
    }
}
