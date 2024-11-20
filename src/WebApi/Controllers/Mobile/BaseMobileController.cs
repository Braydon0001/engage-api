using Microsoft.AspNetCore.Cors;

namespace Engage.WebApi.Controllers.Mobile
{
    [Authorize]
    [EnableCors]
    [ApiController]
    [Route("api/m/[controller]")]
    public class BaseMobileController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        protected const string BadIdText = "The id parameter must be greater than zero";
    }
}
