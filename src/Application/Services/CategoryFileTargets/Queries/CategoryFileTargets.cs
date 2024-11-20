using Engage.Application.Services.CategoryGroups.Queries;
using Engage.Application.Services.EmployeeJobTitles.Queries;
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.EngageRegions.Models;
using Engage.Application.Services.EngageSubGroups.Models;
using Engage.Application.Services.StoreFormats.Queries;
using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.CategoryFileTargets.Queries;

public class CategoryFileTargets
{
    public record CountsRecord(
       int EmployeeJobTitles,
       int Employees,
       int EngageRegions,
       int StoreFormats,
       int Stores,
       int CategoryGroups,
       int EngageSubGroups);

    public CategoryFileTargets(
        IEnumerable<EmployeeJobTitleDto> employeeJobTitles,
        IEnumerable<EmployeeDto> employees,
        IEnumerable<EngageRegionDto> engageRegions,
        IEnumerable<StoreFormatDto> storeFormats,
        IEnumerable<StoreDto> stores,
        IEnumerable<CategoryGroupDto> categoryGroups,
        IEnumerable<EngageSubGroupDto> engageSubGroups)
    {
        Counts = new CountsRecord(
            employeeJobTitles.Count(),
            employees.Count(),
            engageRegions.Count(),
            storeFormats.Count(),
            stores.Count(),
            categoryGroups.Count(),
            engageSubGroups.Count());
        EmployeeJobTitles = employeeJobTitles;
        Employees = employees;
        EngageRegions = engageRegions;
        StoreFormats = storeFormats;
        Stores = stores;
        CategoryGroups = categoryGroups;
        EngageSubGroups = engageSubGroups;
    }

    public CountsRecord Counts { get; }
    public IEnumerable<EmployeeJobTitleDto> EmployeeJobTitles { get; }
    public IEnumerable<EmployeeDto> Employees { get; }
    public IEnumerable<EngageRegionDto> EngageRegions { get; }
    public IEnumerable<StoreFormatDto> StoreFormats { get; }
    public IEnumerable<StoreDto> Stores { get; }
    public IEnumerable<CategoryGroupDto> CategoryGroups { get; }
    public IEnumerable<EngageSubGroupDto> EngageSubGroups { get; }

}