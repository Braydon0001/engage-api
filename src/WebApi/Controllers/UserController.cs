using Engage.Application.Auth.Entities;
using Engage.Application.Services.SecurityRoles.Queries;
using Engage.Application.Services.Users.Commands;
using Engage.Application.Services.Users.Models;
using Engage.Application.Services.Users.Queries;
using Engage.WebApi.utils;
using Okta.Sdk;
using Okta.Sdk.Configuration;

namespace Engage.WebApi.Controllers;

public class UserController : BaseController
{
    private readonly IOptions<JwtOptions> _options;
    private readonly ClerkOptions _clerkOptions;
    private readonly HttpClient _httpClient;

    public UserController(IOptions<JwtOptions> options, IOptions<ClerkOptions> clerkOptions)
    {
        _options = options;
        _clerkOptions = clerkOptions.Value;

        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_clerkOptions.SecretKey}");
        //_httpClient.BaseAddress = new Uri(_clerkOptions.BaseAddress);
    }

    [HttpPost("page")]
    public async Task<ActionResult<ListResult<UserDto>>> PaginatedList(UserPaginatedQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpPost("options/page")]
    public async Task<ActionResult<IEnumerable<UserOption>>> PaginatedOptionList(UserPaginatedOptionQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/project")]
    public async Task<ActionResult<List<OptionDto>>> GetOptionsByProject([FromQuery] ProjectUserOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/store")]
    public async Task<ActionResult<List<OptionDto>>> GetOptionsByStore([FromQuery] StoreUserOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("option/project/supplier")]
    public async Task<ActionResult<List<OptionDto>>> GetOptionsByProjectSupplier([FromQuery] ProjectSupplierUserOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("sync")]
    public async Task<ActionResult<ListResult<UserDto>>> Sync([FromQuery] UserSyncQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("allokta")]
    public async Task<ActionResult<List<IUser>>> GetAllOkta()
    {

        var client = new OktaClient(new OktaClientConfiguration
        {
            OktaDomain = _options.Value.Authority,
            Token = _options.Value.UsersApiToken
        });

        var allUsers = await client.Users.ToListAsync();

        return Ok(allUsers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserVm>> GetVm([FromRoute] UserVmQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult<UserVm>> GetVmByEmail([FromRoute] UserVmEmailQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [AllowAnonymous]
    [HttpGet("employee")]
    public async Task<ActionResult<UserEmployee>> GetEmployeeId([FromRoute] UserEmployeeIdQuery query)
    {
        return Ok(await Mediator.Send(query));
    }


    [HttpGet("option")]
    public async Task<ActionResult<List<UserVm>>> GetOptions([FromQuery] UserOptionsQuery query)
    {
        return Ok(await Mediator.Send(query));
    }

    [HttpGet("clerk/{id}")]
    public async Task<ActionResult<UserClerkVm>> GetUserClerkVm([FromRoute] UserClerkVmQuery query, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(query, cancellationToken));
    }

    [HttpGet("options/page/clerk")]
    public async Task<ActionResult<OptionDto>> PaginatedOptionListClerkId([FromQuery] UsersOptionsWithExternalIdQuery query, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(query, cancellationToken));
    }

    [HttpGet("option/supplierId/{supplierId}")]
    public async Task<ActionResult<List<UserVm>>> GetOptionsBySupplier([FromQuery] UserOptionsQuery query, [FromRoute] int supplierId)
    {
        query.SupplierId = supplierId;
        return Ok(await Mediator.Send(query));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("clerk")]
    public async Task<IActionResult> CreateClerkUser(UserCreateClerkCommand command, CancellationToken cancellationToken)
    {
        //To test: can cause issue where user exists on Clerk & DB but not Okta. To test
        var requestUrl = $"{_httpClient.BaseAddress}/users?email_address={command.Email}";

        //check if email already exists on Clerk
        var returnEmail = await HttpClientUtil.SendGetRequestAsync<List<ClerkUser>>(_httpClient, requestUrl);
        if (returnEmail.Count > 0)
        {
            throw new Exception("Email already exists");
        }

        if (!command.MobileNumber.IsNullOrWhiteSpace())
        {
            requestUrl = $"{_httpClient.BaseAddress}/users?phone_number={command.MobileNumber}";
            var returnNumber = await HttpClientUtil.SendGetRequestAsync<List<ClerkUser>>(_httpClient, requestUrl);

            if (returnNumber.Count > 0)
            {
                throw new Exception("Cell Number has already been used");
            }
        }

        var role = await Mediator.Send(new SecurityRoleVmQuery(command.SecurityRoleId), cancellationToken);
        var metadata = new UserPublicMetadata { Roles = new List<string> { role.Key } };

        requestUrl = $"{_httpClient.BaseAddress}/users";

        var userObject = new UserClerkCreateCommand
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            MobileNumber = command.MobileNumber,
            UserPublicMeta = metadata,
        };

        var returnObject = await HttpClientUtil.SendPostRequestAsync<UserClerkCreateCommand, ClerkUser>(_httpClient, requestUrl, userObject);

        command.ExternalId = returnObject.Id;
        return Ok(await Mediator.Send(command, cancellationToken));

        throw new Exception();
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> CreateBulk(CreateBulkEmployeesAsUsersCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPost("removeBulkGroupsFromJobTitles")]
    public async Task<IActionResult> RemoveBulkGroups(RemoveBulkGroupsFromJobTitlesCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("groups")]
    public async Task<IActionResult> UpdateGroups(UpdateUserGroupsCommand command)
    {
        return Ok(await Mediator.Send(command));
    }

    [HttpPut("clerk")]
    public async Task<IActionResult> UpdateClerk(UserUpdateClerkCommand command, CancellationToken cancellationToken)
    {
        var userData = await Mediator.Send(new UserClerkVmQuery { Id = command.Id }, cancellationToken);

        //TODO: Check if externalId was updated if yes then check valid userId

        if (userData.ExternalId.IsNullOrWhiteSpace())
        {
            throw new Exception("User is missing Clerk Id");
        }

        string requestUrl;
        if (userData.Email.ToLower() != command.Email.ToLower())
        {
            //TODO: Test that user email gets added
            //requestUrl = $"{_clerkOptions.BaseAddress}/email_addresses";

            //var request = new ClerkUserEmailUpdateCommand
            //{
            //    Email = command.Email,
            //    ExternalId = userData.ExternalId,
            //};
            //var returnObject = await HttpClientUtil
            //                        .SendPostRequestAsync<ClerkUserEmailUpdateCommand, ClerkEmailGetQuery>(_httpClient, requestUrl, request);

            //TODO: Remove all alt emails

        }

        //TODO: Check mobile number

        var role = await Mediator.Send(new SecurityRoleWithPermissionsVmQuery { Id = command.SecurityRoleId }, cancellationToken);

        var metadata = new UserPublicMetadata
        {
            Roles = new List<string> { role.Key },
            Permissions = role.SecurityPermissions.Select(e => e.Key).ToList()
        };
        requestUrl = $"{_clerkOptions.BaseAddress}/users/{userData.ExternalId}";

        var body = new UserClerkUpdateCommand
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            UserPublicMeta = metadata
        };

        var updatedUser = await HttpClientUtil.SendPatchRequestAsync<UserClerkUpdateCommand, ClerkUser>(_httpClient, requestUrl, body);

        return Ok(await Mediator.Send(command, cancellationToken));
    }

    [HttpPut("toggledisable")]
    public async Task<IActionResult> ToggleDisable(RemoveUserCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
