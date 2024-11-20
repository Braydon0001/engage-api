using Engage.Application.Services.Manufacturers.Commands;
using Engage.Application.Services.Manufacturers.Models;
using Engage.Application.Services.Manufacturers.Queries;

namespace Engage.WebApi.Controllers
{
    [Authorize("supplier")]
    public class ManufacturerController : BaseController
    {
        [HttpGet("supplierId/{supplierId}")]
        public async Task<ActionResult<ListResult<ManufacturerDto>>> GetAll([FromRoute] ManufacturersQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("variantProduct/{Id}")]
        public async Task<ActionResult<IEnumerable<OptionDto>>> GetByVariantProduct([FromRoute] ManufacurersByVariantProductOptionQuery query, CancellationToken cancellationToken)
        {
            var entities = await Mediator.Send(query, cancellationToken);
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ManufacturerVm>> GetVm([FromRoute] ManufacturerVmQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateManufacturerCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateManufacturerCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
