using Engage.Application.Services.StorePOSFreezerQuestions.Models;
using Engage.Application.Services.StorePOSFreezerQuestions.Queries;
using Engage.Application.Services.StorePOSQuestions.Commands;

namespace Engage.WebApi.Controllers;

public class StorePOSFreezerQuestionController : BaseController
{
    [HttpGet("store/{storeId}")]
    public async Task<ActionResult<ListResult<StorePOSFreezerQuestionDto>>> GetAll([FromRoute] GetStorePOSFreezerQuestionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StorePOSFreezerQuestionVm>> GetVm([FromRoute] GetStorePOSFreezerQuestionVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStorePOSFreezerQuestionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateStorePOSFreezerQuestionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
