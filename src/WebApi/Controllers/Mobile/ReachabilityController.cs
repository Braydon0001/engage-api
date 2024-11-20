namespace Engage.WebApi.Controllers.Mobile;

public class ReachabilityController : BaseMobileController
{
    [AllowAnonymous]
    [HttpHead]
    public NoContentResult Reachable()
    {
        return NoContent();
    }
}
