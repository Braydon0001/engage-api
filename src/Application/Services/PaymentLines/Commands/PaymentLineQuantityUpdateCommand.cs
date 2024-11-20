namespace Engage.Application.Services.PaymentLines.Commands;

public class PaymentLineQuantityUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int Quantity { get; set; }
}
public record PaymentLineQuantityUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentLineQuantityUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(PaymentLineQuantityUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.PaymentLines.FirstOrDefaultAsync(e => e.PaymentLineId == command.Id, cancellationToken);
        entity.Quantity = command.Quantity;

        var opStatus = await Context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.PaymentLineId;
        return opStatus;
    }
}
public class PaymentLineQuantityUpdateValidator : AbstractValidator<PaymentLineQuantityUpdateCommand>
{
    public PaymentLineQuantityUpdateValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Quantity).NotEmpty();
    }
}