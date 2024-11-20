using Engage.Application.Services.VatPeriods.Queries;

namespace Engage.Application.Services.Vat.Queries;

public class VatAmountQuery : IRequest<decimal>
{
    public int VatId { get; set; }
    public int? OverrideVatId { get; set; }
    public decimal Amount { get; set; }

}

public class VatAmountQueryHandler : IRequestHandler<VatAmountQuery, decimal>
{

    private readonly IMediator _mediator;

    public VatAmountQueryHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<decimal> Handle(VatAmountQuery request, CancellationToken cancellationToken)
    {
        if (request.OverrideVatId.HasValue && request.VatId != request.OverrideVatId)
        {
            var overideVatPercent = await _mediator.Send(new VatPeriodPercentQuery(request.OverrideVatId.Value));
            if (overideVatPercent == 0)
            {
                return 0;
            }
        }

        var vatPercent = await _mediator.Send(new VatPeriodPercentQuery(request.VatId));

        return request.Amount * vatPercent / 100;
    }
}
