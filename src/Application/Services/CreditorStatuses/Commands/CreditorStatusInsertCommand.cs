namespace Engage.Application.Services.CreditorStatuses.Commands;

public class CreditorStatusInsertCommand : IMapTo<CreditorStatus>, IRequest<CreditorStatus>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorStatusInsertCommand, CreditorStatus>();
    }
}

public record CreditorStatusInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorStatusInsertCommand, CreditorStatus>
{
    public async Task<CreditorStatus> Handle(CreditorStatusInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CreditorStatusInsertCommand, CreditorStatus>(command);
        
        Context.CreditorStatuses.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CreditorStatusInsertValidator : AbstractValidator<CreditorStatusInsertCommand>
{
    public CreditorStatusInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}