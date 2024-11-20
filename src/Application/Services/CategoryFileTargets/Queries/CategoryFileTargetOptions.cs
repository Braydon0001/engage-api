using Engage.Application.Services.CategoryGroups.Queries;
using Engage.Application.Services.EmployeeJobTitles.Queries;
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.EngageRegions.Models;
using Engage.Application.Services.EngageSubGroups.Models;
using Engage.Application.Services.StoreFormats.Queries;
using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.CategoryFileTargets.Queries;

public class CategoryFileTargetOptions
{
    public record CountsRecord(
       int EmployeeJobTitles,
       int Employees,
       int EngageRegions,
       int StoreFormats,
       int Stores,
       int CategoryGroups,
       int EngageSubGroups);

    public CategoryFileTargetOptions(
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
        EmployeeJobTitles = employeeJobTitles.Select(e => new OptionDto() { Id = e.Id, Name = e.Name });
        Employees = employees.Select(e => new OptionDto() { Id = e.Id, Name = e.FirstName + " " + e.LastName });
        EngageRegions = engageRegions.Select(e => new OptionDto() { Id = e.Id, Name = e.Name });
        StoreFormats = storeFormats.Select(e => new OptionDto() { Id = e.Id, Name = e.Name });
        Stores = stores.Select(e => new OptionDto() { Id = e.Id, Name = e.Name });
        CategoryGroups = categoryGroups.Select(e => new OptionDto() { Id = e.Id, Name = e.Name });
        EngageSubGroups = engageSubGroups.Select(e => new OptionDto() { Id = e.Id, Name = e.Name });
    }

    public CountsRecord Counts { get; }
    public IEnumerable<OptionDto> EmployeeJobTitles { get; }
    public IEnumerable<OptionDto> Employees { get; }
    public IEnumerable<OptionDto> EngageRegions { get; }
    public IEnumerable<OptionDto> StoreFormats { get; }
    public IEnumerable<OptionDto> Stores { get; }
    public IEnumerable<OptionDto> CategoryGroups { get; }
    public IEnumerable<OptionDto> EngageSubGroups { get; }

}