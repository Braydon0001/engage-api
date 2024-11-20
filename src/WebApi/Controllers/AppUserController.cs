using Engage.Application.Services.AppUsers.Commands;

namespace Engage.WebApi.Controllers;

public class AppUserController : BaseController
{
    [AllowAnonymous]
    [HttpPost("EmailLogin")]
    public async Task<OperationStatus> EmailLogin(LoginUserEmailCommand command)
    {
        return await Mediator.Send(command);
    }

    [HttpPost("UserId")]
    public async Task<OperationStatus> UserId(GetUserIdCommand command)
    {
        return await Mediator.Send(command);
    }

    [AllowAnonymous]
    [HttpPost("CheckMobileVersion")]
    public async Task<OperationStatus> CheckMobileVersion(CheckMobileVersionCommand command)
    {
        return await Mediator.Send(command);
    }
}
