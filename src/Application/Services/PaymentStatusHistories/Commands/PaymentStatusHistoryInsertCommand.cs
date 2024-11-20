namespace Engage.Application.Services.PaymentStatusHistories.Commands;

public class PaymentStatusHistoryInsertCommand : IMapTo<PaymentStatusHistory>, IRequest<OperationStatus>
{
    public int PaymentId { get; init; }
    public int PaymentStatusId { get; init; }
    public string Reason { get; init; }
    public bool SaveChanges { get; set; } = true;
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentStatusHistoryInsertCommand, PaymentStatusHistory>();
    }
}

public record PaymentStatusHistoryInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentStatusHistoryInsertCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(PaymentStatusHistoryInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<PaymentStatusHistoryInsertCommand, PaymentStatusHistory>(command);

        Context.PaymentStatusHistories.Add(entity);

        if (command.SaveChanges)
        {
            var opStatus = await Context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = command.PaymentId;
            return opStatus;
        }

        return new OperationStatus(status: true);
    }
}

public class PaymentStatusHistoryInsertValidator : AbstractValidator<PaymentStatusHistoryInsertCommand>
{
    public PaymentStatusHistoryInsertValidator()
    {
        RuleFor(e => e.PaymentId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.PaymentStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Reason).MaximumLength(300);
    }
}