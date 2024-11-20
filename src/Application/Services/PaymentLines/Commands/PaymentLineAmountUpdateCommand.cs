using Engage.Application.Services.Vat.Models;
using Engage.Application.Services.Vat.Queries;

namespace Engage.Application.Services.PaymentLines.Commands;

public class PaymentLineAmountUpdateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    //public int VatId { get; set; }
}
public record PaymentLineAmountUpdateHandler(IAppDbContext Context, IMapper Mapper, IMediator Mediator) : IRequestHandler<PaymentLineAmountUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(PaymentLineAmountUpdateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await Context.PaymentLines.FirstOrDefaultAsync(e => e.PaymentLineId == command.Id, cancellationToken);

            entity.Amount = (float)command.Amount;

            var vatAmountQuery = new VatAmountQuery
            {
                VatId = entity.IsVat ? (int)VatId.Standard : (int)VatId.ZeroRated,
                Amount = command.Amount
            };

            var vatAmount = await Mediator.Send(vatAmountQuery);
            entity.VatAmount = (float)vatAmount;

            var opStatus = await Context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.PaymentLineId;
            opStatus.ReturnObject = new VatAmountDto
            {
                Amount = (decimal)entity.Amount,
                VatAmount = (decimal)entity.VatAmount,
                TotalAmount = (decimal)entity.Amount + (decimal)entity.VatAmount
            };

            return opStatus;
        }
        catch (VatException ex)
        {
            return OperationStatus.CreateFromException(ex.Message, ex);
        }
    }
}
public class PaymentLineAmountUpdateValidator : AbstractValidator<PaymentLineAmountUpdateCommand>
{
    public PaymentLineAmountUpdateValidator()
    {
        RuleFor(e => e.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Amount).NotEmpty();
        //RuleFor(e => e.VatId).NotEmpty().GreaterThan(0);
    }
}