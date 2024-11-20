namespace Engage.Application.Services.SparSources.Commands;

public class SparSourceInsertCommand : IMapTo<SparSource>, IRequest<SparSource>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparSourceInsertCommand, SparSource>();
    }
}

public record SparSourceInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparSourceInsertCommand, SparSource>
{
    public async Task<SparSource> Handle(SparSourceInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SparSourceInsertCommand, SparSource>(command);
        
        Context.SparSources.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SparSourceInsertValidator : AbstractValidator<SparSourceInsertCommand>
{
    public SparSourceInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
    }
}