using Engage.Application.Services.Users.Queries;
using System.Text.Json.Serialization;

namespace Engage.Application.Services.SecurityOrganizations.Commands;

public class ClerkOrganizationRequest
{
    public ClerkOrganizationRequest(string name, string userId)
    {
        Name = name;
        UserId = userId;
    }

    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("created_by")]
    public string UserId { get; set; }
}

public class ClerkOrganizationResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("slug")]
    public string Slug { get; set; }
}

public class SecurityOrganizationInsertCommand : IMapTo<SecurityOrganization>, IRequest<SecurityOrganization>
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public string ExternalId { get; set; }
    public string UserId { get; set; }
    public int OwnerId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SecurityOrganizationInsertCommand, SecurityOrganization>();
    }
}

public record SecurityOrganizationInsertHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator, IClerkHttpClient ClerkClient) : IRequestHandler<SecurityOrganizationInsertCommand, SecurityOrganization>
{
    public async Task<SecurityOrganization> Handle(SecurityOrganizationInsertCommand command, CancellationToken cancellationToken)
    {
        var user = await Mediator.Send(new UserClerkVmQuery { Id = command.OwnerId }, cancellationToken);

        var request = new ClerkOrganizationRequest(command.Name, user.ExternalId);

        var response = await ClerkClient.PostAsync<ClerkOrganizationRequest, ClerkOrganizationResponse>("organizations", request);

        var entity = Mapper.Map<SecurityOrganizationInsertCommand, SecurityOrganization>(command);
        entity.Slug = response.Slug;
        entity.ExternalId = response.Id;

        Context.SecurityOrganizations.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SecurityOrganizationInsertValidator : AbstractValidator<SecurityOrganizationInsertCommand>
{
    public SecurityOrganizationInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(200);
        //RuleFor(e => e.Slug).NotEmpty().MaximumLength(200);
        RuleFor(e => e.OwnerId).NotEmpty().GreaterThan(0);
    }
}