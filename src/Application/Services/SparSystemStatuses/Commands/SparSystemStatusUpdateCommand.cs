namespace Engage.Application.Services.SparSystemStatuses.Commands;

public class SparSystemStatusUpdateCommand : IMapTo<SparSystemStatus>, IRequest<SparSystemStatus>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparSystemStatusUpdateCommand, SparSystemStatus>();
    }
}

public record SparSystemStatusUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparSystemStatusUpdateCommand, SparSystemStatus>
{
    public async Task<SparSystemStatus> Handle(SparSystemStatusUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SparSystemStatuses.SingleOrDefaultAsync(e => e.SparSystemStatusId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSparSystemStatusValidator : AbstractValidator<SparSystemStatusUpdateCommand>
{
    public UpdateSparSystemStatusValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
    }
}