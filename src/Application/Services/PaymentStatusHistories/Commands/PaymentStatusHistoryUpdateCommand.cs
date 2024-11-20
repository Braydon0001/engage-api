namespace Engage.Application.Services.PaymentStatusHistories.Commands;

public class PaymentStatusHistoryUpdateCommand : IMapTo<PaymentStatusHistory>, IRequest<PaymentStatusHistory>
{
    public int Id { get; set; }
    public int PaymentId { get; init; }
    public int PaymentStatusId { get; init; }
    public string Reason { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<PaymentStatusHistoryUpdateCommand, PaymentStatusHistory>();
    }
}

public record PaymentStatusHistoryUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentStatusHistoryUpdateCommand, PaymentStatusHistory>
{
    public async Task<PaymentStatusHistory> Handle(PaymentStatusHistoryUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.PaymentStatusHistories.SingleOrDefaultAsync(e => e.PaymentStatusHistoryId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdatePaymentStatusHistoryValidator : AbstractValidator<PaymentStatusHistoryUpdateCommand>
{
    public UpdatePaymentStatusHistoryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.PaymentId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.PaymentStatusId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Reason).MaximumLength(300);
    }
}