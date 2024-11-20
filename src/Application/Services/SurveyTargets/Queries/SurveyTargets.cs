using Engage.Application.Services.EmployeeJobTitles.Queries;
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.EngageRegions.Models;
using Engage.Application.Services.StoreFormats.Queries;
using Engage.Application.Services.Stores.Queries;

namespace Engage.Application.Services.SurveyTargets.Queries;

public class SurveyTargets
{
    public record CountsRecord(
       int EmployeeJobTitles,
       int Employees,
       int EngageRegions,
       int StoreFormats,
       int Stores);

    public SurveyTargets(
        IEnumerable<EmployeeJobTitleDto> employeeJobTitles,
        IEnumerable<EmployeeDto> employees,
        IEnumerable<EngageRegionDto> engageRegions,
        IEnumerable<StoreFormatDto> storeFormats,
        IEnumerable<StoreDto> stores)
    {
        Counts = new CountsRecord(
            employeeJobTitles.Count(),
            employees.Count(),
            engageRegions.Count(),
            storeFormats.Count(),
            stores.Count());
        EmployeeJobTitles = employeeJobTitles;
        Employees = employees;
        EngageRegions = engageRegions;
        StoreFormats = storeFormats;
        Stores = stores;
    }

    public CountsRecord Counts { get; }
    public IEnumerable<EmployeeJobTitleDto> EmployeeJobTitles { get; }
    public IEnumerable<EmployeeDto> Employees { get; }
    public IEnumerable<EngageRegionDto> EngageRegions { get; }
    public IEnumerable<StoreFormatDto> StoreFormats { get; }
    public IEnumerable<StoreDto> Stores { get; }

}
