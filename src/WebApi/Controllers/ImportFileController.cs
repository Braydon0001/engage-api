using Engage.Application.Services.ImportFiles;

namespace Engage.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImportFileController : BaseController
{
    [HttpGet]
    public async Task<ActionResult<ListResult<ImportFileListDto>>> Get([FromRoute] ImportFilesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ImportFileDto>> GetById([FromRoute] ImportFileQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateImportFileCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateImportFileCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
