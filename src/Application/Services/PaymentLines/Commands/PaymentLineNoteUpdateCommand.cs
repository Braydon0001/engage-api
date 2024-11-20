namespace Engage.Application.Services.PaymentLines.Commands;

public class PaymentLineNoteUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public string Note { get; set; }
}
public record PaymentLineNoteUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<PaymentLineNoteUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(PaymentLineNoteUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.PaymentLines.FirstOrDefaultAsync(e => e.PaymentLineId == command.Id, cancellationToken);
        entity.Note = command.Note;

        var opStatus = await Context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.PaymentLineId;
        return opStatus;
    }
}
public class PaymentLineNoteUpdateValidator : AbstractValidator<PaymentLineNoteUpdateCommand>
{
    public PaymentLineNoteUpdateValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Note).NotEmpty().MaximumLength(1000);
    }
}