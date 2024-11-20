using Engage.Application.Services.EmployeeDivisions.Queries;
using Engage.Application.Services.EmployeeJobTitles.Queries;
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.EngageDepartments.Models;
using Engage.Application.Services.EngageRegions.Models;
using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.SurveyFormTargets.Queries;

public class SurveyFormTargets
{
    public record CountsRecord(
       int Employees,
       int EmployeeEngageRegions,
       int EngageDepartments,
       int EmployeeJobTitles,
       int EmployeeDivisions,
       int Stores,
       int StoreEngageRegions,
       int StoreClusters,
       int StoreFormats,
       int StoreLSMs,
       int StoreTypes
        );

    public SurveyFormTargets(
        IEnumerable<EmployeeDto> employees,
        IEnumerable<EngageRegionDto> employeeEngageRegions,
        IEnumerable<EngageDepartmentDto> engageDepartments,
        IEnumerable<EmployeeJobTitleDto> employeeJobTitles,
        IEnumerable<EmployeeDivisionDto> employeeDivisions,
        IEnumerable<StoreDto> stores,
        IEnumerable<EngageRegionDto> storeEngageRegions,
        IEnumerable<OptionDto> storeClusters,
        IEnumerable<OptionDto> storeFormats,
        IEnumerable<OptionDto> storeLSMs,
        IEnumerable<OptionDto> storeTypes
        )
    {
        Counts = new CountsRecord(
            employees.Count(),
            employeeEngageRegions.Count(),
            engageDepartments.Count(),
            employeeJobTitles.Count(),
            employeeDivisions.Count(),
            stores.Count(),
            storeEngageRegions.Count(),
            storeClusters.Count(),
            storeFormats.Count(),
            storeLSMs.Count(),
            storeTypes.Count()
            );
        Employees = employees;
        EmployeeEngageRegions = employeeEngageRegions;
        EngageDepartments = engageDepartments;
        EmployeeJobTitles = employeeJobTitles;
        EmployeeDivisions = employeeDivisions;
        Stores = stores;
        StoreEngageRegions = storeEngageRegions;
        StoreClusters = storeClusters;
        StoreFormats = storeFormats;
        StoreLSMs = storeLSMs;
        StoreTypes = storeTypes;
    }

    public CountsRecord Counts { get; }
    public IEnumerable<EmployeeDto> Employees { get; }
    public IEnumerable<EngageRegionDto> EmployeeEngageRegions { get; }
    public IEnumerable<EngageDepartmentDto> EngageDepartments { get; }
    public IEnumerable<EmployeeJobTitleDto> EmployeeJobTitles { get; }
    public IEnumerable<EmployeeDivisionDto> EmployeeDivisions { get; }
    public IEnumerable<StoreDto> Stores { get; }
    public IEnumerable<EngageRegionDto> StoreEngageRegions { get; }
    public IEnumerable<OptionDto> StoreClusters { get; }
    public IEnumerable<OptionDto> StoreFormats { get; }
    public IEnumerable<OptionDto> StoreLSMs { get; }
    public IEnumerable<OptionDto> StoreTypes { get; }

}