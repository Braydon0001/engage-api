using Engage.Application.Services.EmployeeHrStatistic.Queries;
using Engage.Application.Services.EmployeeHrStatistics.Model;
using Engage.Application.Services.EmployeeStores.Models;
using Engage.Application.Services.EmployeeStores.Queries;

namespace Engage.WebApi.Controllers;

public class EmployeeHrStatisticController : BaseController
{
    [HttpGet()]
    public async Task<ActionResult<EmployeeHrStatisticsDto>> Get([FromQuery] EmployeeHrStatisticsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("departmentgroups/headcount")]
    public async Task<ActionResult<DepartmentGroupHeadCountDto>> GetAllDepartmentHeadcounts([FromQuery] DepartmentHeadCountsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("training-cost-ytd")]
    public async Task<ActionResult<StatCardDto>> GetTrainingCostFinYtd([FromQuery] TrainingCostFinYtdQuery query)
    {
        return Ok(await Mediator.Send(query));
    }
}
