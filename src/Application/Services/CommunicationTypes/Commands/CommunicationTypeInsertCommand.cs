namespace Engage.Application.Services.CommunicationTypes.Commands;

public class CommunicationTypeInsertCommand : IMapTo<CommunicationType>, IRequest<CommunicationType>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CommunicationTypeInsertCommand, CommunicationType>();
    }
}

public record CommunicationTypeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationTypeInsertCommand, CommunicationType>
{
    public async Task<CommunicationType> Handle(CommunicationTypeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CommunicationTypeInsertCommand, CommunicationType>(command);

        Context.CommunicationTypes.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CommunicationTypeInsertValidator : AbstractValidator<CommunicationTypeInsertCommand>
{
    public CommunicationTypeInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}