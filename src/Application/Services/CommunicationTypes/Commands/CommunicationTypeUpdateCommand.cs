namespace Engage.Application.Services.CommunicationTypes.Commands;

public class CommunicationTypeUpdateCommand : IMapTo<CommunicationType>, IRequest<CommunicationType>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CommunicationTypeUpdateCommand, CommunicationType>();
    }
}

public record CommunicationTypeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CommunicationTypeUpdateCommand, CommunicationType>
{
    public async Task<CommunicationType> Handle(CommunicationTypeUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CommunicationTypes.SingleOrDefaultAsync(e => e.CommunicationTypeId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCommunicationTypeValidator : AbstractValidator<CommunicationTypeUpdateCommand>
{
    public UpdateCommunicationTypeValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}