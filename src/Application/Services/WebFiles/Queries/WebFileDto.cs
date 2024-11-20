// auto-generated

namespace Engage.Application.Services.WebFiles.Queries;

public class WebFileDto : IMapFrom<WebFile>
{
    public int Id { get; set; }
    public int WebFileCategoryId { get; set; }
    public string WebFileCategoryName { get; set; }
    public string WebFileGroupName { get; set; }
    public int FileTypeId { get; set; }
    public string FileTypeName { get; set; }
    public int TargetStrategyId { get; set; }
    public string TargetStrategyName { get; set; }
    public int? EmployeeId { get; set; }
    public string EmployeeFullName { get; set; }
    public int? StoreId { get; set; }
    public string StoreName { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public DateTime Created { get; set; }
    public List<JsonFile> Files { get; set; }
    public string FileUrl { get; set; }
    public string FileName { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebFile, WebFileDto>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.WebFileId))
               .ForMember(d => d.WebFileGroupName, opt => opt.MapFrom(s => s.WebFileCategory.WebFileGroup.Name))
               .ForMember(d => d.EmployeeFullName, opt => opt.MapFrom(s => $"{s.Employee.FirstName} {s.Employee.LastName}"));
    }
}
