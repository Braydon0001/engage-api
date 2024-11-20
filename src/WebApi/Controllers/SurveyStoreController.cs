using Engage.Application.Services.ImportSurveyStores;
using Engage.Application.Services.Stores.Models;
using Engage.Application.Services.SurveyStores;
using Microsoft.Extensions.Primitives;

namespace Engage.WebApi.Controllers;

[Authorize("survey")]
public class SurveyStoreController : BaseController
{
    [HttpPost("import")]
    public async Task<IActionResult> Import(IFormCollection form)
    {
        form.TryGetValue("surveyId", out StringValues surveyIdString);
        var surveyId = Convert.ToInt32(surveyIdString);

        form.TryGetValue("storeFormatIds", out StringValues storeFormatIdsString);
        var storeFormatIds = DelimitedStringToList(storeFormatIdsString.ToString());

        return Ok(await Mediator.Send(new ImportSurveyStoresCommand
        {
            SurveyId = surveyId,
            StoreFormatIds = storeFormatIds,
            File = form.Files[0]
        }));
    }

    [HttpPost("processimport")]
    public async Task<IActionResult> ProcessImport(ProcessSurveyStoresImportCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
    private List<int> DelimitedStringToList(string str)
    {
        return !string.IsNullOrWhiteSpace(str)
            ? str.Split(",")
                 .Where(id => !string.IsNullOrWhiteSpace(id))
                 .Select(id => Convert.ToInt32(id))
                 .ToList()
            : new List<int>();
    }

    [HttpGet("survey/{surveyId}")]
    public async Task<ActionResult<PaginatedListResult<StoreListDto>>> GetBySurvey([FromQuery] GetQuery query, [FromRoute] int surveyId)
    {
        return Ok(await Mediator.Send(query.Merge(new SurveyStoresQuery
        {
            SurveyId = surveyId
        })));
    }

    [HttpPost("stores")]
    public async Task<IActionResult> CreateSurveyStores(CreateSurveyStoresCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("criteria")]
    public async Task<IActionResult> CreateSurveyStoresWithCriteria(CreateSurveyStoresWithCriteriaCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("delete")]
    public async Task<IActionResult> DeleteSurveyStore(DeleteSurveyStoreCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpDelete("batch/survey/{surveyId}")]
    public async Task<IActionResult> BatchDeleteSurveyStore([FromRoute] BatchDeleteSurveyStoresCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
