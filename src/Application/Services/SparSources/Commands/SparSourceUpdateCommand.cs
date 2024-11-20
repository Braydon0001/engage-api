namespace Engage.Application.Services.SparSources.Commands;

public class SparSourceUpdateCommand : IMapTo<SparSource>, IRequest<SparSource>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparSourceUpdateCommand, SparSource>();
    }
}

public record SparSourceUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparSourceUpdateCommand, SparSource>
{
    public async Task<SparSource> Handle(SparSourceUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.SparSources.SingleOrDefaultAsync(e => e.SparSourceId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateSparSourceValidator : AbstractValidator<SparSourceUpdateCommand>
{
    public UpdateSparSourceValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty();
    }
}