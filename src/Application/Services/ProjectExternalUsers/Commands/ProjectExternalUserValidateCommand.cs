namespace Engage.Application.Services.ProjectExternalUsers.Commands;

public class ProjectExternalUserValidateCommand : IRequest<bool>
{
    public string Email { get; init; }
}

public record ProjectExternalUserValidateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectExternalUserValidateCommand, bool>
{
    public async Task<bool> Handle(ProjectExternalUserValidateCommand command, CancellationToken cancellationToken)
    {
        var previousEntityEmail = await Context.ProjectExternalUsers.FirstOrDefaultAsync(e => e.Email.ToLower() == command.Email.ToLower(), cancellationToken);

        var user = await Context.Users.FirstOrDefaultAsync(e => e.Email.ToLower() == command.Email.ToLower(), cancellationToken);

        var contact = await Context.EntityContacts.FirstOrDefaultAsync(e => e.EmailAddress1.ToLower() == command.Email.ToLower(), cancellationToken);

        if (previousEntityEmail != null || user != null || contact != null)
        {
            return false;
        }

        return true;

    }
}

public class ProjectExternalUserValidateValidator : AbstractValidator<ProjectExternalUserValidateCommand>
{
    public ProjectExternalUserValidateValidator()
    {
        RuleFor(e => e.Email).NotEmpty();
    }
}