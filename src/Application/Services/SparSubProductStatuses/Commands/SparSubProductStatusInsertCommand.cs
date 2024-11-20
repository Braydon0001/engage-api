namespace Engage.Application.Services.SparSubProductStatuses.Commands;

public class SparSubProductStatusInsertCommand : IMapTo<SparSubProductStatus>, IRequest<SparSubProductStatus>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<SparSubProductStatusInsertCommand, SparSubProductStatus>();
    }
}

public record SparSubProductStatusInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<SparSubProductStatusInsertCommand, SparSubProductStatus>
{
    public async Task<SparSubProductStatus> Handle(SparSubProductStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<SparSubProductStatusInsertCommand, SparSubProductStatus>(command);
        
        Context.SparSubProductStatuses.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class SparSubProductStatusInsertValidator : AbstractValidator<SparSubProductStatusInsertCommand>
{
    public SparSubProductStatusInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty();
    }
}