namespace Engage.Application.Services.SparProductStatuses.Commands;

public class SparProductStatusInsertCommand : IMapTo<SparProductStatus>, IRequest<SparProductStatus>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparProductStatusInsertCommand, SparProductStatus>();
    }
}

public record SparProductStatusInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparProductStatusInsertCommand, SparProductStatus>
{
    public async Task<SparProductStatus> Handle(SparProductStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SparProductStatusInsertCommand, SparProductStatus>(command);
        
        Context.SparProductStatuses.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SparProductStatusInsertValidator : AbstractValidator<SparProductStatusInsertCommand>
{
    public SparProductStatusInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
    }
}