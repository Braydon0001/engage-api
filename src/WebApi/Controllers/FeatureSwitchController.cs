namespace Engage.WebApi.Controllers;
public class FeatureSwitchController : BaseController
{
    [HttpGet]
    public ActionResult<FeatureSwitchOptions> GetAll(IOptions<FeatureSwitchOptions> featureSwitch)
    {
        return Ok(featureSwitch.Value);
    }
}
