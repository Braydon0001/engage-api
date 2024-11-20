using Engage.Application.Services.Surveys.Commands;
using Engage.Application.Services.Surveys.Models;
using Engage.Application.Services.Surveys.Queries;

namespace Engage.WebApi.Controllers;

[Authorize("survey")]
public class SurveyController : BaseController
{
    [HttpPost("page")]
    public async Task<ActionResult<ListResult<SurveyListDto>>> PaginatedQuery(SurveyPaginatedQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SurveyVm>> GetVm([FromRoute] SurveyVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateSurveyCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Put(UpdateSurveyCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpGet]
    [Route("StoreUploadVM/{id}")]
    public async Task<ActionResult<SurveyTargetingVM>> StoreUploadVM([FromRoute] SurveyStoreUploadVMQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet]
    [Route("TargetingVM/{id}")]
    public async Task<ActionResult<SurveyTargetingVM>> TargetingVM([FromRoute] SurveyTargetingVMQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    [Route("engageregion/batchassign")]
    public async Task<IActionResult> BatchAssignEngageRegions(BatchAssignSurveyEngageRegionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost]
    [Route("engageregion/assign")]
    public async Task<IActionResult> AssignEngageRegion(AssignSurveyEngageRegionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost]
    [Route("engageregion/unassign")]
    public async Task<IActionResult> UnassignEngageRegion(UnassignSurveyEngageRegionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost]
    [Route("store/batchassign")]
    public async Task<IActionResult> BatchAssignStores(BatchAssignSurveyStoreCommand command)
    {
        return Ok(await Mediator.Send(command));
    }


}
