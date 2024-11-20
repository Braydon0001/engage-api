namespace Engage.Application.Services.SparSystemStatuses.Commands;

public class SparSystemStatusInsertCommand : IMapTo<SparSystemStatus>, IRequest<SparSystemStatus>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparSystemStatusInsertCommand, SparSystemStatus>();
    }
}

public record SparSystemStatusInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparSystemStatusInsertCommand, SparSystemStatus>
{
    public async Task<SparSystemStatus> Handle(SparSystemStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SparSystemStatusInsertCommand, SparSystemStatus>(command);
        
        Context.SparSystemStatuses.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SparSystemStatusInsertValidator : AbstractValidator<SparSystemStatusInsertCommand>
{
    public SparSystemStatusInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
    }
}