using Engage.Application.Services.ProjectStakeholders.Queries;

namespace Engage.Application.Services.ProjectExternalUsers.Commands;

public class ProjectExternalUserInsertCommand : IMapTo<ProjectExternalUser>, IRequest<OperationStatus>
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string CellNumber { get; init; }
    public bool EmailPrimary { get; init; }
    public bool PhoneNumberPrimary { get; init; }
    public int? ExternalUserTypeId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectExternalUserInsertCommand, ProjectExternalUser>();
    }
}

public record ProjectExternalUserInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectExternalUserInsertCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(ProjectExternalUserInsertCommand command, CancellationToken cancellationToken)
    {
        var previousEntityEmail = await Context.ProjectExternalUsers.FirstOrDefaultAsync(e => e.Email.ToLower() == command.Email.ToLower(), cancellationToken);

        var user = await Context.Users.FirstOrDefaultAsync(e => e.Email.ToLower() == command.Email.ToLower(), cancellationToken);

        var contact = await Context.EntityContacts.FirstOrDefaultAsync(e => e.EmailAddress1.ToLower() == command.Email.ToLower(), cancellationToken);

        if (previousEntityEmail != null || user != null || contact != null)
        {
            throw new Exception("User already exists with this email");
        }
        if (command.CellNumber.IsNotNullOrWhiteSpace())
        {
            var previouseEntityEmail = await Context.ProjectExternalUsers.FirstOrDefaultAsync(e => e.CellNumber.ToLower() == command.CellNumber.ToLower(), cancellationToken);

            if (previouseEntityEmail != null)
            {
                throw new Exception("User with this cellphone number already exsits");
            }
        }

        var entity = Mapper.Map<ProjectExternalUserInsertCommand, ProjectExternalUser>(command);

        Context.ProjectExternalUsers.Add(entity);

        var opStatus = await Context.SaveChangesAsync(cancellationToken);

        if (command.EmailPrimary)
        {
            Context.ProjectExternalUserCommunicationTypes.Add(new ProjectExternalUserCommunicationType
            {
                ProjectExternalUserId = entity.ProjectExternalUserId,
                CommunicationTypeId = (int)CommunicationTypeId.Email,
            });
        }

        if (command.PhoneNumberPrimary)
        {
            Context.ProjectExternalUserCommunicationTypes.Add(new ProjectExternalUserCommunicationType
            {
                ProjectExternalUserId = entity.ProjectExternalUserId,
                CommunicationTypeId = (int)CommunicationTypeId.WhatsApp,
            });
        }

        await Context.SaveChangesAsync(cancellationToken);

        opStatus.OperationId = entity.ProjectExternalUserId;

        opStatus.ReturnObject = new ProjectStakeholderSearchOption
        {
            Id = entity.ProjectExternalUserId,
            Name = $"{entity.FirstName} {entity.LastName}",
            Identifier = "externalUser"
        };

        return opStatus;
    }
}

public class ProjectExternalUserInsertValidator : AbstractValidator<ProjectExternalUserInsertCommand>
{
    public ProjectExternalUserInsertValidator()
    {
        RuleFor(e => e.FirstName).NotEmpty();
        RuleFor(e => e.LastName).NotEmpty();
        RuleFor(e => e.Email).NotEmpty();
        RuleFor(e => e.CellNumber);
    }
}