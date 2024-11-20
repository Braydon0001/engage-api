using Engage.Application.Services.Vacancies.Commands;
using Engage.Application.Services.Vacancies.Models;
using Engage.Application.Services.Vacancies.Queries;

namespace Engage.WebApi.Controllers;

public class VacancyController : BaseController
{

    [HttpGet]
    public async Task<ActionResult<ListResult<VacancyDto>>> GetAll([FromRoute] VacanciesQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<VacancyVm>> GetVm([FromRoute] VacancyVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateVacancyCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateVacancyCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

}
