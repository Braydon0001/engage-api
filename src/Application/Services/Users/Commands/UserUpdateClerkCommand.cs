using Engage.Application.Auth.Entities;
using Engage.Application.Services.SecurityRoleUsers.Commands;
using System.Text.Json.Serialization;

namespace Engage.Application.Services.Users.Commands;

public class UserUpdateClerkCommand : IRequest<OperationStatus>, IMapTo<User>
{
    public int Id { get; set; }
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
public record UserUpdateClerkHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<UserUpdateClerkCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(UserUpdateClerkCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.Users.SingleOrDefaultAsync(e => e.UserId == command.Id);

        var mappedEntity = Mapper.Map(command, entity);

        await Mediator.Send(new SecurityRoleUserUpdateUserRolesCommand
        {
            RoleId = command.SecurityRoleId,
            UserId = entity.UserId,
            Save = false
        }, cancellationToken);

        await Context.SaveChangesAsync(cancellationToken);

        return new(entity.UserId);

    }
}

public class UserUpdateClerkValidator : AbstractValidator<UserUpdateClerkCommand>
{
    public UserUpdateClerkValidator()
    {
        RuleFor(e => e.Id).NotNull().GreaterThan(0);
        RuleFor(e => e.FirstName).NotEmpty().MaximumLength(120);
        RuleFor(e => e.LastName).NotEmpty().MaximumLength(120);
        RuleFor(e => e.MobileNumber).MaximumLength(30);
        RuleFor(e => e.Email).NotEmpty().MaximumLength(100);
        RuleFor(e => e.SecurityRoleId).NotNull().GreaterThan(0);
    }
}
public class UserClerkUpdateCommand
{
    //public int Id { get; set; }
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }
    [JsonPropertyName("public_metadata")]
    public UserPublicMetadata UserPublicMeta { get; set; }

}

