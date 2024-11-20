using Engage.Application.Services.Mobile.Surveys.Queries;
using Engage.Application.Services.StoreSurveys.Models;
using Engage.Application.Services.StoreSurveys.Queries;
using Engage.Application.Services.SurveyInstances.Commands;

namespace Engage.WebApi.Controllers.Mobile
{
    [AllowAnonymous]
    public class SurveyController : BaseMobileController
    {
        private readonly FeatureSwitchOptions _options;
        private readonly IOptions<ImageOptions> _imageSettings;

        public SurveyController(IOptions<FeatureSwitchOptions> options, IOptions<ImageOptions> imageSettings)
        {
            _options = options.Value;
            _imageSettings = imageSettings;
        }

        [HttpGet]
        public async Task<ActionResult<DataResult<StoreSurveysDto>>> GetAll([FromRoute] GetStoreSurveys query, [FromQuery] int employeeId, [FromQuery] int storeId, [FromQuery] DateTime timezoneDate)
        {
            if (_options.IsNewSurveyTargeting)
            {
                return Ok(await Mediator.Send(new SurveyTargetedQuery
                {
                    EmployeeId = employeeId,
                    StoreId = storeId,
                    TimezoneDate = timezoneDate
                }));
            }

            return Ok(await Mediator.Send(new GetTargetedSurveys2
            {
                EmployeeId = employeeId,
                StoreId = storeId,
                TimezoneDate = timezoneDate
            }));
        }

        [HttpGet("GetAllSurveysByEmployeeRegion")]
        public async Task<ActionResult<DataResult<List<StoreSurveysDto>>>> GetAllSurveysByEmployeeRegion([FromRoute] GetStoreSurveysByEmployeeRegionQuery query, [FromQuery] int employeeId, [FromQuery] DateTime timezoneDate)
        {
            query.EmployeeId = employeeId;
            query.TimezoneDate = timezoneDate;
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("GetSurveysByEmployeeStoreHistory")]
        public async Task<ActionResult<DataResult<List<StoreSurveysDto>>>> GetSurveysByEmployeeStoreHistory([FromRoute] GetSurveysByEmployeeStoreHistoryQuery query, [FromQuery] int employeeId, [FromQuery] DateTime timezoneDate)
        {
            query.EmployeeId = employeeId;
            query.TimezoneDate = timezoneDate;
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSurveyInstanceCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.SurveyPhotoFolder))
            {
                command.SurveyPhotoFolder = _imageSettings.Value.SurveyPhotoFolder;
                command.SurveyPhotoFolderPath = _imageSettings.Value.SurveyPhotoFolderPath;
            }

            return Ok(await Mediator.Send(command));
        }
    }
}

