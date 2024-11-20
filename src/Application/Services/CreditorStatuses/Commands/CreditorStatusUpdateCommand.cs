namespace Engage.Application.Services.CreditorStatuses.Commands;

public class CreditorStatusUpdateCommand : IMapTo<CreditorStatus>, IRequest<CreditorStatus>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorStatusUpdateCommand, CreditorStatus>();
    }
}

public record CreditorStatusUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorStatusUpdateCommand, CreditorStatus>
{
    public async Task<CreditorStatus> Handle(CreditorStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CreditorStatuses.SingleOrDefaultAsync(e => e.CreditorStatusId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCreditorStatusValidator : AbstractValidator<CreditorStatusUpdateCommand>
{
    public UpdateCreditorStatusValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}