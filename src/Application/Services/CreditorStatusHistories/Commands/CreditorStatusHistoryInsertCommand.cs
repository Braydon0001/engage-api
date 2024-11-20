namespace Engage.Application.Services.CreditorStatusHistories.Commands;

public class CreditorStatusHistoryInsertCommand : IMapTo<CreditorStatusHistory>, IRequest<OperationStatus>
{
    public int CreditorId { get; init; }
    public int CreditorStatusId { get; init; }
    public string Reason { get; init; }
    public bool SaveChanges { get; set; } = true;
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreditorStatusHistoryInsertCommand, CreditorStatusHistory>();
    }
}

public record CreditorStatusHistoryInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CreditorStatusHistoryInsertCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(CreditorStatusHistoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CreditorStatusHistoryInsertCommand, CreditorStatusHistory>(command);

        Context.CreditorStatusHistories.Add(entity);

        if (command.SaveChanges)
        {
            var opStatus = await Context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = command.CreditorId;
            return opStatus;
        }

        return new OperationStatus(status: true);
    }
}

public class CreditorStatusHistoryInsertValidator : AbstractValidator<CreditorStatusHistoryInsertCommand>
{
    public CreditorStatusHistoryInsertValidator()
    {
        RuleFor(e => e.CreditorId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CreditorStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Reason).MaximumLength(300);
    }
}