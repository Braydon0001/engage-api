using Engage.Application.Services.EmployeeAssets.Models;
using Engage.Application.Services.EmployeeCoolerBoxes.Models;
using Engage.Application.Services.EmployeeVehicles.Models;

namespace Engage.Application.Services.EmployeeWeb.Models;

public class EmployeeWebAssetsVm : IMapFrom<Employee>
{
    public ICollection<EmployeeAssetDto> EmployeeAssets { get; set; }
    public ICollection<EmployeeVehicleDto> EmployeeVehicles { get; set; }
    public ICollection<EmployeeCoolerBoxDto> EmployeeCoolerBoxes { get; set; }
}
