using Engage.Application.Services.EmployeeDivisions.Queries;
using Engage.Application.Services.EmployeeJobTitles.Queries;
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.EngageDepartments.Models;
using Engage.Application.Services.EngageRegions.Models;
using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.SurveyFormTargets.Queries;

public class SurveyFormTargetOptions
{
    public record CountsRecord(
       int Employees,
       int EmployeeEngageRegions,
       int EmployeeEngageDepartments,
       int EmployeeJobTitles,
       int EmployeeDivisions,
       int Stores,
       int StoreEngageRegions,
       int StoreClusters,
       int StoreFormats,
       int StoreLSMs,
       int StoreTypes);

    public SurveyFormTargetOptions(
        IEnumerable<EmployeeDto> employees,
        IEnumerable<EngageRegionDto> employeeEngageRegions,
        IEnumerable<EngageDepartmentDto> employeeEngageDepartments,
        IEnumerable<EmployeeJobTitleDto> employeeJobTitles,
        IEnumerable<EmployeeDivisionDto> employeeDivisions,
        IEnumerable<StoreDto> stores,
        IEnumerable<EngageRegionDto> storeEngageRegions,
        IEnumerable<OptionDto> storeClusters,
        IEnumerable<OptionDto> storeFormats,
        IEnumerable<OptionDto> storeLSMs,
        IEnumerable<OptionDto> storeTypes)
    {
        Counts = new CountsRecord(
            employees.Count(),
            employeeEngageRegions.Count(),
            employeeEngageDepartments.Count(),
            employeeJobTitles.Count(),
            employeeDivisions.Count(),
            stores.Count(),
            storeEngageRegions.Count(),
            storeClusters.Count(),
            storeFormats.Count(),
            storeLSMs.Count(),
            storeTypes.Count());

        Employees = employees.Select(e => new OptionDto() { Id = e.Id, Name = e.FirstName + " " + e.LastName });
        EmployeeEngageRegions = employeeEngageRegions.Select(e => new OptionDto() { Id = e.Id, Name = e.Name });
        EmployeeEngageDepartments = employeeEngageDepartments.Select(e => new OptionDto() { Id = e.Id, Name = e.Name });
        EmployeeJobTitles = employeeJobTitles.Select(e => new OptionDto() { Id = e.Id, Name = e.Name });
        EmployeeDivisions = employeeDivisions.Select(e => new OptionDto() { Id = e.Id, Name = e.Name });
        Stores = stores.Select(e => new OptionDto() { Id = e.Id, Name = e.Name });
        StoreEngageRegions = storeEngageRegions.Select(e => new OptionDto() { Id = e.Id, Name = e.Name });
        StoreClusters = storeClusters.Select(e => new OptionDto() { Id = e.Id, Name = e.Name });
        StoreFormats = storeFormats.Select(e => new OptionDto() { Id = e.Id, Name = e.Name });
        StoreLSMs = storeLSMs.Select(e => new OptionDto() { Id = e.Id, Name = e.Name });
        StoreTypes = storeTypes.Select(e => new OptionDto() { Id = e.Id, Name = e.Name });
    }

    public CountsRecord Counts { get; }
    public IEnumerable<OptionDto> Employees { get; }
    public IEnumerable<OptionDto> EmployeeEngageRegions { get; }
    public IEnumerable<OptionDto> EmployeeEngageDepartments { get; }
    public IEnumerable<OptionDto> EmployeeJobTitles { get; }
    public IEnumerable<OptionDto> EmployeeDivisions { get; }
    public IEnumerable<OptionDto> Stores { get; }
    public IEnumerable<OptionDto> StoreEngageRegions { get; }
    public IEnumerable<OptionDto> StoreClusters { get; }
    public IEnumerable<OptionDto> StoreFormats { get; }
    public IEnumerable<OptionDto> StoreLSMs { get; }
    public IEnumerable<OptionDto> StoreTypes { get; }
}