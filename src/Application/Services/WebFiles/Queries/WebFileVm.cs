// auto-generated
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.FileTypes.Queries;
using Engage.Application.Services.Stores.Queries;
using Engage.Application.Services.TargetStrategies.Queries;
using Engage.Application.Services.WebFileCategories.Queries;
using Engage.Application.Services.WebFileGroups.Queries;

namespace Engage.Application.Services.WebFiles.Queries;

public class WebFileVm : IMapFrom<WebFile>
{
    public int Id { get; set; }
    public WebFileGroupOption WebFileGroupId { get; set; }
    public WebFileCategoryOption WebFileCategoryId { get; set; }
    public FileTypeOption FileTypeId { get; set; }
    public TargetStrategyOption TargetStrategyId { get; set; }
    public EmployeeOption EmployeeId { get; set; }
    public StoreOption StoreId { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<JsonFile> Files { get; set; }
    public string FileUrl { get; set; }
    public string FileName { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebFile, WebFileVm>()
               .ForMember(d => d.Id, opt => opt.MapFrom(s => s.WebFileId))
               .ForMember(d => d.WebFileGroupId, opt => opt.MapFrom(s => s.WebFileCategory.WebFileGroup))
               .ForMember(d => d.WebFileCategoryId, opt => opt.MapFrom(s => s.WebFileCategory))
               .ForMember(d => d.FileTypeId, opt => opt.MapFrom(s => s.FileType))
               .ForMember(d => d.TargetStrategyId, opt => opt.MapFrom(s => s.TargetStrategy))
               .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.Employee))
               .ForMember(d => d.StoreId, opt => opt.MapFrom(s => s.Store));
    }
}
