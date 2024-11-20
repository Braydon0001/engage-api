namespace Engage.Application.Services.ProductPeriods.Queries;

public class ProductPeriodCurrentPreviousIdQuery : IRequest<ProductPeriodCurrentPreviousIdDto>
{
}
public record ProductPeriodCurrentPreviousIdHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<ProductPeriodCurrentPreviousIdQuery, ProductPeriodCurrentPreviousIdDto>
{
    public async Task<ProductPeriodCurrentPreviousIdDto> Handle(ProductPeriodCurrentPreviousIdQuery request, CancellationToken cancellationToken)
    {
        var currenPeriod = await Context.ProductPeriods.AsNoTracking()
                                                        .Where(e => e.StartDate.Date <= DateTime.Now.Date
                                                               && e.EndDate.Date >= DateTime.Now.Date)
                                                        .FirstOrDefaultAsync(cancellationToken)
                                                            ?? throw new Exception("Current Period Not Found");
        var previousPeriodDate = currenPeriod.StartDate.AddDays(-2);
        var previousPeriod = await Context.ProductPeriods.AsNoTracking()
                                                          .Where(e => e.StartDate.Date <= previousPeriodDate.Date
                                                            && e.EndDate.Date >= previousPeriodDate.Date)
                                                          .FirstOrDefaultAsync(cancellationToken)
                                                            ?? throw new Exception("No previous period found");
        return new(previousPeriod.ProductPeriodId, currenPeriod.ProductPeriodId);
    }
}