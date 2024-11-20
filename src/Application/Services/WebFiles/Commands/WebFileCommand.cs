namespace Engage.Application.Services.WebFiles.Commands;

public class WebFileCommand : IMapTo<WebFile>
{
    public int WebFileCategoryId { get; set; }
    public int FileTypeId { get; set; }
    public int TargetStrategyId { get; set; }
    public int? EmployeeId { get; set; }
    public int? StoreId { get; set; }
    public string Name { get; set; }
    public string DisplayName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string FileUrl { get; set; }
    public bool IsNPrinted { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<WebFileCommand, WebFile>();
    }
}
