using Engage.Application.Auth.Entities;
using Engage.Application.Interfaces;
using Engage.Application.Services.SecurityOrganizations.Commands;
using Engage.Application.Services.SecurityOrganizations.Queries;
using Engage.WebApi.utils;

namespace Engage.WebApi.Controllers;

public partial class SecurityOrganizationController : BaseController
{
    private readonly ClerkOptions _options;
    private readonly HttpClient _httpClient;
    //private readonly IUserService _user;
    public SecurityOrganizationController(IOptions<ClerkOptions> options, IUserService user)
    {
        _options = options.Value;
        //_user = user;

        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_options.SecretKey}");
        _httpClient.BaseAddress = new Uri(_options.BaseAddress);
    }
    [HttpGet]
    public async Task<ActionResult<ListResult<SecurityOrganizationDto>>> List([FromQuery] SecurityOrganizationListQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(new ListResult<SecurityOrganizationDto>(entities));
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SecurityOrganizationOption>>> Options([FromQuery] SecurityOrganizationOptionQuery query, CancellationToken cancellationToken)
    {
        var entities = await Mediator.Send(query, cancellationToken);

        return Ok(entities);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SecurityOrganizationVm>> Vm([FromRoute] int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
        {
            return BadRequest(BadIdText);
        }

        var entity = await Mediator.Send(new SecurityOrganizationVmQuery(id), cancellationToken);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Insert(SecurityOrganizationInsertCommand command, CancellationToken cancellationToken)
    {
        //string requestUrl = $"{_options.BaseAddress}/organizations";

        //var user = await Mediator.Send(new UserClerkVmQuery { Id = command.OwnerId }, cancellationToken);

        //if (user == null || user.ExternalId.IsNullOrWhiteSpace())
        //{
        //    throw new Exception("User not found");
        //}

        //var request = new SecurityOrganizationClerkCreateCommand
        //{
        //    //MembershipMax = 10,
        //    Name = command.Name,
        //    UserId = user.ExternalId
        //};

        //var returnObject = await HttpClientUtil
        //    .SendPostRequestAsync<SecurityOrganizationClerkCreateCommand, ClerkOrganization>(_httpClient, requestUrl, request);

        //command.Slug = returnObject.Slug;
        //command.ExternalId = returnObject.Id;
        var entity = await Mediator.Send(command, cancellationToken);

        return Ok(new OperationStatus(entity.SecurityOrganizationId));
    }

    [HttpPut]
    public async Task<IActionResult> Update(SecurityOrganizationUpdateCommand command, CancellationToken cancellationToken)
    {
        string requestUrl = $"{_options.BaseAddress}/organizations/{command.ExternalId}";

        var request = new SecurityOrganizationClerkCreateCommand
        {
            Name = command.Name,
            Slug = command.Slug,
        };

        var returnObject = await HttpClientUtil
            .SendPatchRequestAsync<SecurityOrganizationClerkCreateCommand, ClerkOrganization>(_httpClient, requestUrl, request);

        var entity = await Mediator.Send(command, cancellationToken);

        return entity == null ? NotFound() : Ok(new OperationStatus(entity.SecurityOrganizationId));
    }

}
