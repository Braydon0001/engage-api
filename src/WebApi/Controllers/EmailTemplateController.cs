using Engage.Application.Services.EmailTemplates.Commands;
using Engage.Application.Services.EmailTemplates.Models;
using Engage.Application.Services.EmailTemplates.Queries;

namespace Engage.WebApi.Controllers;

public class EmailTemplateController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<EmailTemplateDto>>> GetAll([FromRoute] EmailTemplatesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("emailtemplatetype/{emailtemplatetypeid}")]
    public async Task<ActionResult<ListResult<EmailTemplateDto>>> GetAllByEmailTemplateTypeId([FromRoute] EmailTemplatesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EmailTemplateVm>> GetVm([FromRoute] EmailTemplateVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/emailtemplates")]
    public async Task<ActionResult<List<OptionDto>>> ClaimTemplatesOptionsQuery([FromRoute] EmailTemplateOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmailTemplateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateEmailTemplateCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
