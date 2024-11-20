
using Engage.Application.Auth.Entities;
using Engage.Application.Services.SecurityRoleUsers.Commands;
using System.Text.Json.Serialization;

namespace Engage.Application.Services.Users.Commands;

public class UserCreateClerkCommand : IRequest<User>, IMapTo<User>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MobileNumber { get; set; }
    public int SupplierId { get; set; }
    public string ExternalId { get; set; }
    public string Email { get; set; }
    public int SecurityRoleId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UserUpdateClerkCommand, User>();
    }
}
public record UserCreateClerkHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<UserCreateClerkCommand, User>
{
    public async Task<User> Handle(UserCreateClerkCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<UserCreateClerkCommand, User>(command);

        Context.Users.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        await Mediator.Send(new SecurityRoleUserUpdateUserRolesCommand
        {
            RoleId = command.SecurityRoleId,
            UserId = entity.UserId,
        }, cancellationToken);

        return entity;
    }
}
public class UserClerkCreateCommand
{
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }
    [JsonPropertyName("phone_number")]
    public string MobileNumber { get; set; }
    [JsonPropertyName("public_metadata")]
    public UserPublicMetadata UserPublicMeta { get; set; }
}
public class UserCreateClerkValidator : AbstractValidator<UserCreateClerkCommand>
{
    public UserCreateClerkValidator()
    {
        RuleFor(e => e.FirstName).NotEmpty().MaximumLength(120);
        RuleFor(e => e.LastName).NotEmpty().MaximumLength(120);
        RuleFor(e => e.MobileNumber).MaximumLength(30);
        RuleFor(e => e.Email).NotEmpty().MaximumLength(100);
        RuleFor(e => e.SecurityRoleId).NotNull().GreaterThan(0);
    }
}