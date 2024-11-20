namespace Engage.Application.Services.CategoryFiles.Queries;

public class CategoryFileDto : IMapFrom<CategoryFile>
{
    public int Id { get; init; }
    public int CategoryFileTypeId { get; init; }
    public string CategoryFileTypeName { get; init; }
    public int? StoreId { get; init; }
    public string StoreName { get; init; }
    public int? CategoryGroupId { get; init; }
    public string CategoryGroupName { get; init; }
    public int? CategorySubGroupId { get; init; }
    public string CategorySubGroupName { get; init; }
    public string Name { get; init; }
    public string Note { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public List<JsonFile> Files { get; init; }
    public JsonRule TargetRule { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CategoryFile, CategoryFileDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.CategoryFileId));
    }
}

public class CategoryFileAdvancedTargetingVm
{
    public bool HasAdvancedTargeting { get; init; }
    public List<int> EmployeeId { get; init; }
    public List<int> EmployeeJobTitleIds { get; init; }
    public List<int> EmployeeSubGroupIds { get; init; }
    public List<int> EmployeeEngageRegionIds { get; init; }
    public List<int> StoreId { get; init; }
    public List<int> StoreFormatIds { get; init; }
    public List<int> StoreCategoryGroupIds { get; init; }
    public ListResult<CategoryFileDto> CategoryFiles { get; init; }
}
