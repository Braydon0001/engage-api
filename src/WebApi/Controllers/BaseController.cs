using AutoMapper;
using Engage.Application.Interfaces;

namespace Engage.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    protected IAppDbContext Context
    {
        get
        {
            return HttpContext.RequestServices.GetService<IAppDbContext>()!;
        }
    }

    protected IMapper Mapper
    {
        get
        {
            return HttpContext.RequestServices.GetService<IMapper>();
        }
    }

    protected const string BadIdText = "The id parameter must be greater than zero";

    [HttpDelete("{id}")]
    [Route("[controller]/[action]/{id}")]
    public virtual async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new DeleteCommand
        {
            EntityName = ControllerContext.ActionDescriptor.ControllerName,
            Id = id
        }));
    }

    [HttpDelete("undo/{id}")]
    [Route("[controller]/[action]/{id}")]
    public virtual async Task<IActionResult> UndoDelete([FromRoute] int id)
    {
        return Ok(await Mediator.Send(new DeleteCommand
        {
            EntityName = ControllerContext.ActionDescriptor.ControllerName,
            Id = id,
            Undo = true
        }));
    }

    [HttpPut("toggledisabled")]
    public virtual async Task<IActionResult> ToggleDisabled(DisableCommand disableCommand)
    {
        return Ok(await Mediator.Send(new DeleteCommand
        {
            EntityName = ControllerContext.ActionDescriptor.ControllerName,
            Id = disableCommand.Id,
            Toggle = true
        }));
    }


}
