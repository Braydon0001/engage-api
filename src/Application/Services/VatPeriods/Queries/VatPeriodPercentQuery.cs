namespace Engage.Application.Services.VatPeriods.Queries;

public class VatPeriodPercentQuery : IRequest<int>
{
    public int VatId { get; private set; }

    public VatPeriodPercentQuery(int vatId)
    {
        VatId = vatId;
    }
}

public class VatPeriodPercentQueryHandler : IRequestHandler<VatPeriodPercentQuery, int>
{
    private readonly IAppDbContext _context;

    public VatPeriodPercentQueryHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(VatPeriodPercentQuery request, CancellationToken cancellationToken)
    {
        var vatPeriod = await _context.VatPeriods.Where(e => e.VatId == request.VatId &&
                                                                      DateTime.UtcNow.Date >= e.StartDate.Date)
                                                          .OrderByDescending(e => e.StartDate)
                                                          .FirstOrDefaultAsync(cancellationToken);
        if (vatPeriod == null)
        {
            throw new VatException($"There is no Vat Period for today's date. (Vat Id: {request.VatId})");
        }

        return vatPeriod.Percent;

    }
}
