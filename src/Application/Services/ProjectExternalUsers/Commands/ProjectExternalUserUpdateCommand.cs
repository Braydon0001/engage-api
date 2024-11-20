namespace Engage.Application.Services.ProjectExternalUsers.Commands;

public class ProjectExternalUserUpdateCommand : IMapTo<ProjectExternalUser>, IRequest<ProjectExternalUser>
{
    public int Id { get; set; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string CellNumber { get; init; }
    public bool EmailPrimary { get; init; }
    public bool PhoneNumberPrimary { get; init; }
    public int? ExternalUserTypeId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<ProjectExternalUserUpdateCommand, ProjectExternalUser>();
    }
}

public record ProjectExternalUserUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProjectExternalUserUpdateCommand, ProjectExternalUser>
{
    public async Task<ProjectExternalUser> Handle(ProjectExternalUserUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.ProjectExternalUsers.SingleOrDefaultAsync(e => e.ProjectExternalUserId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        var communicationTypes = await Context.ProjectExternalUserCommunicationTypes.Where(e => e.ProjectExternalUserId == command.Id).ToListAsync(cancellationToken);

        if (command.EmailPrimary)
        {
            if (!communicationTypes.Any(e => e.CommunicationTypeId == (int)CommunicationTypeId.Email))
            {
                Context.ProjectExternalUserCommunicationTypes.Add(new ProjectExternalUserCommunicationType
                {
                    ProjectExternalUserId = mappedEntity.ProjectExternalUserId,
                    CommunicationTypeId = (int)CommunicationTypeId.Email,
                });
            }
        }
        else
        {
            var emailComm = communicationTypes.FirstOrDefault(e => e.CommunicationTypeId == (int)CommunicationTypeId.Email);
            if (emailComm != null)
            {
                Context.ProjectExternalUserCommunicationTypes.Remove(emailComm);
            }
        }

        if (command.PhoneNumberPrimary)
        {
            if (!communicationTypes.Any(e => e.CommunicationTypeId == (int)CommunicationTypeId.WhatsApp))
            {
                Context.ProjectExternalUserCommunicationTypes.Add(new ProjectExternalUserCommunicationType
                {
                    ProjectExternalUserId = mappedEntity.ProjectExternalUserId,
                    CommunicationTypeId = (int)CommunicationTypeId.WhatsApp,
                });
            }
        }
        else
        {
            var phone = communicationTypes.FirstOrDefault(e => e.CommunicationTypeId == (int)CommunicationTypeId.WhatsApp);
            if (phone != null)
            {
                Context.ProjectExternalUserCommunicationTypes.Remove(phone);
            }
        }

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateProjectExternalUserValidator : AbstractValidator<ProjectExternalUserUpdateCommand>
{
    public UpdateProjectExternalUserValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.FirstName).NotEmpty();
        RuleFor(e => e.LastName).NotEmpty();
        RuleFor(e => e.Email);
        RuleFor(e => e.CellNumber);
    }
}