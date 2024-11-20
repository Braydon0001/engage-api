using Engage.Application.Services.Projects.Queries;
using Engage.Application.Services.ProjectStores.Commands;
using Engage.Application.Services.ProjectStores.Queries;


namespace Engage.WebApi.Controllers
{
    public partial class ProjectStoreController : BaseController
    {
        [HttpPost("page")]
        public async Task<ActionResult<ListResult<ProjectDto>>> Paginated(ProjectStorePaginatedQuery query, CancellationToken cancellationToken)
        {
            var entities = await Mediator.Send(query, cancellationToken);

            return Ok(new ListResult<ProjectDto>(entities));
        }

        [HttpPost("grid/page")]
        public async Task<ActionResult<ListResult<ProjectDto>>> GridPaginated(ProjectStoreGridPaginatedQuery query, CancellationToken cancellationToken)
        {
            var entities = await Mediator.Send(query, cancellationToken);

            return Ok(new ListResult<ProjectDto>(entities));
        }

        [HttpPost("store/{storeid}")]
        public async Task<ActionResult<ListResult<ProjectDto>>> ProjectsByStoreId([FromRoute] int storeId, CancellationToken cancellationToken)
        {
            if (storeId <= 0)
            {
                return BadRequest(BadIdText);
            }
            var entities = await Mediator.Send(new ProjectStorePaginatedQuery { StoreId = storeId }, cancellationToken);

            return Ok(new ListResult<ProjectDto>(entities));
        }

        [HttpPost("user/{userId}")]
        public async Task<ActionResult<ListResult<ProjectDto>>> ProjectsByUserId([FromRoute] int userId, CancellationToken cancellationToken)
        {
            if (userId <= 0)
            {
                return BadRequest(BadIdText);
            }
            var entities = await Mediator.Send(new ProjectStorePaginatedQuery { UserId = userId }, cancellationToken);

            return Ok(new ListResult<ProjectDto>(entities));
        }

        [AllowAnonymous]
        [HttpGet("external/stakeholder")]
        public async Task<ActionResult<ProjectStoreExternalUserVm>> GetExternalUserDetails([FromQuery] ProjectStoreExternalUserVmQuery query, CancellationToken cancellationToken)
        {
            if (query.ProjectId <= 0 || query.ProjectStakeholderId <= 0)
            {
                return BadRequest("No ProjectId");
            }
            var entity = await Mediator.Send(query, cancellationToken);

            return Ok(entity);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectStoreVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(BadIdText);
            }

            var entity = await Mediator.Send(new ProjectStoreVmQuery(id), cancellationToken);

            return entity == null ? NotFound() : Ok(entity);
        }

        [HttpGet("dashboard")]
        public async Task<ActionResult<List<ProjectStoreDashboardDto>>> Dashboard([FromQuery] ProjectStoreDashboardQuery query, CancellationToken cancellationToken)
        {

            var entity = await Mediator.Send(query, cancellationToken);

            return Ok(entity);
        }

        [HttpGet("add")]
        public async Task<ActionResult<ProjectStoreCreateVm>> Vm([FromQuery] ProjectStoreCreateVmQuery query, CancellationToken cancellationToken)
        {
            var entity = await Mediator.Send(query, cancellationToken);

            return entity == null ? NotFound() : Ok(entity);
        }

        [AllowAnonymous]
        [HttpGet("external/{id}")]
        public async Task<ActionResult<ProjectStoreVm>> ExternalVm([FromRoute] int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return BadRequest(BadIdText);
            }

            var entity = await Mediator.Send(new ProjectStoreVmQuery(id, true), cancellationToken);

            return entity == null ? NotFound() : Ok(entity);
        }

        [AllowAnonymous]
        [HttpGet("option")]
        public async Task<ActionResult<List<OptionDto>>> GetByType([FromRoute] ProjectStoreOptionsQuery query, [FromQuery] string search, [FromQuery] int storeId, bool isRegional)
        {
            query.Search = search;
            query.IsRegional = isRegional;
            query.StoreId = storeId;
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("option/{id}")]
        public async Task<ActionResult<OptionDto>> GetOptionVm([FromQuery] ProjectStoreOptionVmQuery query, [FromRoute] int id, CancellationToken cancellationToken)
        {
            query.Id = id;
            return Ok(await Mediator.Send(query, cancellationToken));
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ProjectStoreInsertCommand command, CancellationToken cancellationToken)
        {
            var opStatus = await Mediator.Send(command, cancellationToken);

            return Ok(opStatus);
        }

        [AllowAnonymous]
        [HttpPost("communication")]
        public async Task<IActionResult> SendCommunication(ProjectStoreSendCommunicationCommand command, CancellationToken cancellationToken)
        {
            var optStatus = await Mediator.Send(command, cancellationToken);

            return Ok(new OperationStatus(optStatus.Status));
        }

        [HttpPost("quick")]
        public async Task<IActionResult> QuickInsert(ProjectStoreQuickInsertCommand command, CancellationToken cancellationToken)
        {
            var opStatus = await Mediator.Send(command, cancellationToken);

            return Ok(opStatus);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProjectStoreUpdateCommand command, CancellationToken cancellationToken)
        {
            var entity = await Mediator.Send(command, cancellationToken);

            return entity == null ? NotFound() : Ok(entity);
        }

        [AllowAnonymous]
        [HttpPut("external")]
        public async Task<IActionResult> UpdateExternal(ProjectStoreExternalUpdateCommand command, CancellationToken cancellationToken)
        {
            var entity = await Mediator.Send(command, cancellationToken);

            return entity == null ? NotFound() : Ok(entity);
        }

        [HttpPost("bulkinsert")]
        public async Task<IActionResult> BulkInsert(ProjectStoreBulkInsertCommand command, CancellationToken cancellationToken)
        {
            var insertedIds = await Mediator.Send(command, cancellationToken);

            return insertedIds == null ? NotFound() : Ok(new OperationStatus(insertedIds.Count, command.ProjectId));
        }

        [HttpPut("bulkdelete")]
        public async Task<IActionResult> BulkDelete(ProjectStoreBulkDeleteCommand command, CancellationToken cancellationToken)
        {
            var deleteCount = await Mediator.Send(command, cancellationToken);

            return !deleteCount.HasValue ? NotFound() : Ok(new OperationStatus(deleteCount.Value, command.ProjectId));
        }
    }
}
