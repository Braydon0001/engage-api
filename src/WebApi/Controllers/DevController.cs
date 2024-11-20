namespace Engage.WebApi.Controllers;

[Authorize]
public class DevController : ControllerBase
{

    public string GetValues()
    {
        return "Value";
    }
}
