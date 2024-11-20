using Engage.Application.Services.EmployeeDivisions.Queries;
using Engage.Application.Services.EmployeeJobTitles.Queries;
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.EngageDepartments.Models;
using Engage.Application.Services.EngageRegions.Models;
using Engage.Application.Services.StoreFormats.Queries;
using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.WebFileTargets.Queries;

public class WebFileTargets
{
    public record CountsRecord(
        int EmployeeDivisions,
        int EngageDepartments,
        int EmployeeJobTitles,
        int Employees,
        int EngageRegions,
        int StoreFormats,
        int Stores);

    public WebFileTargets(
        IEnumerable<EmployeeDivisionDto> employeeDivisions,
        IEnumerable<EngageDepartmentDto> engageDepartments,
        IEnumerable<EmployeeJobTitleDto> employeeJobTitles,
        IEnumerable<EmployeeDto> employees,
        IEnumerable<EngageRegionDto> engageRegions,
        IEnumerable<StoreFormatDto> storeFormats,
        IEnumerable<StoreDto> stores)
    {
        Counts = new CountsRecord(
            employeeDivisions.Count(),
            engageDepartments.Count(),
            employeeJobTitles.Count(),
            employees.Count(),
            engageRegions.Count(),
            storeFormats.Count(),
            stores.Count());
        EmployeeDivisions = employeeDivisions;
        EngageDepartments = engageDepartments;
        EmployeeJobTitles = employeeJobTitles;
        Employees = employees;
        EngageRegions = engageRegions;
        StoreFormats = storeFormats;
        Stores = stores;
    }

    public CountsRecord Counts { get; }
    public IEnumerable<EngageDepartmentDto> EngageDepartments { get; }
    public IEnumerable<EmployeeDivisionDto> EmployeeDivisions { get; }
    public IEnumerable<EmployeeJobTitleDto> EmployeeJobTitles { get; }
    public IEnumerable<EmployeeDto> Employees { get; }
    public IEnumerable<EngageRegionDto> EngageRegions { get; }
    public IEnumerable<StoreFormatDto> StoreFormats { get; }
    public IEnumerable<StoreDto> Stores { get; }

}