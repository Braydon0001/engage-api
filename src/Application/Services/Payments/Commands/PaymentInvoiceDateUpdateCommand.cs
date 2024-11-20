namespace Engage.Application.Services.Payments.Commands;

public class PaymentInvoiceDateUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public DateTime InvoiceDate { get; set; }
}
public record PaymentInvoiceDateUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentInvoiceDateUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(PaymentInvoiceDateUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.Payments.FirstOrDefaultAsync(e => e.PaymentId == command.Id, cancellationToken);
        entity.InvoiceDate = command.InvoiceDate;

        var opStatus = await Context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.PaymentId;
        return opStatus;
    }
}
public class PaymentInvoiceDateUpdateValidator : AbstractValidator<PaymentInvoiceDateUpdateCommand>
{
    public PaymentInvoiceDateUpdateValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.InvoiceDate).NotEmpty();
    }
}