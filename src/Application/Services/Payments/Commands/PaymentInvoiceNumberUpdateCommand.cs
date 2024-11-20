namespace Engage.Application.Services.Payments.Commands;

public class PaymentInvoiceNumberUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; }
}
public record PaymentInvoiceNumberUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentInvoiceNumberUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(PaymentInvoiceNumberUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.Payments.FirstOrDefaultAsync(e => e.PaymentId == command.Id, cancellationToken);
        entity.InvoiceNumber = command.InvoiceNumber;

        var opStatus = await Context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.PaymentId;
        return opStatus;
    }
}
public class PaymentInvoiceNumberUpdateValidator : AbstractValidator<PaymentInvoiceNumberUpdateCommand>
{
    public PaymentInvoiceNumberUpdateValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.InvoiceNumber).NotEmpty().MaximumLength(200);
    }
}