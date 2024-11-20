using Engage.Application.Services.StorePOSFreezerQuestions.Commands;
using Engage.Application.Services.StorePOSQuestions.Models;
using Engage.Application.Services.StorePOSQuestions.Queries;

namespace Engage.WebApi.Controllers;

public class StorePOSQuestionController : BaseController
{
    [HttpGet("store/{storeId}")]
    public async Task<ActionResult<ListResult<StorePOSQuestionDto>>> GetAll([FromRoute] GetStorePOSQuestionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StorePOSQuestionVm>> GetVm([FromRoute] GetStorePOSQuestionVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateStorePOSQuestionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateStorePOSQuestionCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
