namespace Engage.Application.Services.CreditorStatusHistories.Commands;

public class CreditorStatusHistoryUpdateCommand : IMapTo<CreditorStatusHistory>, IRequest<CreditorStatusHistory>
{
    public int Id { get; set; }
    public int CreditorId { get; init; }
    public int CreditorStatusId { get; init; }
    public string Reason { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorStatusHistoryUpdateCommand, CreditorStatusHistory>();
    }
}

public record CreditorStatusHistoryUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorStatusHistoryUpdateCommand, CreditorStatusHistory>
{
    public async Task<CreditorStatusHistory> Handle(CreditorStatusHistoryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CreditorStatusHistories.SingleOrDefaultAsync(e => e.CreditorStatusHistoryId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCreditorStatusHistoryValidator : AbstractValidator<CreditorStatusHistoryUpdateCommand>
{
    public UpdateCreditorStatusHistoryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CreditorId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CreditorStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Reason).MaximumLength(300);
    }
}