using Engage.Application.Services.Orders.Models;

namespace Engage.Application.Services.Orders.Commands;

public class OrderGenerateSummaryPdfCommand : IRequest<MemoryStream>
{
    public int Id { get; set; }
}
public record OrderGenerateSummaryPdfhandler(IAppDbContext Context, IMapper Mapper, IPdfService PdfService, IOptions<OrderDefaultsOptions> Options, IOptions<ContactReportSettings> ContactReportSettings) : IRequestHandler<OrderGenerateSummaryPdfCommand, MemoryStream>
{
    public async Task<MemoryStream> Handle(OrderGenerateSummaryPdfCommand query, CancellationToken cancellationToken)
    {
        var order = await Context.Orders
                           .AsNoTracking()
                           .Where(e => e.OrderId == query.Id)
                           .ProjectTo<OrderSummaryPdfVm>(Mapper.ConfigurationProvider)
                           .FirstOrDefaultAsync(cancellationToken);

        var skus = await Context.OrderSkus
                               .AsNoTracking()
                               .Where(e => e.OrderId == query.Id)
                               .ProjectTo<OrderSummarySkusPdfVm>(Mapper.ConfigurationProvider)
                               .ToListAsync(cancellationToken);

        var productSkus = skus.Where(e => e.OrderSkuTypeId == Options.Value.SkuTypeId)
                              .GroupBy(e => e.OrderQuantityTypeName)
                              .OrderBy(group => group.Key)
                              .ToDictionary(group => group.Key, group => group.ToList());

        productSkus = productSkus.OrderByDescending(e => e.Value.Sum(d => d.Quantity)).ToDictionary(e => e.Key, e => e.Value);

        var freeTextSkus = skus.Where(e => e.OrderSkuTypeId == Options.Value.DescriptionSkuTypeId)
                               .ToList();

        order.OrderSkus = new OrderSummarySkusByQuantityTypePdfDto { ProductSkus = productSkus, FreeTextSkus = freeTextSkus };

        var currentUser = await Context.Users.Where(e => e.Email == order.CreatedBy)
                                                      .FirstOrDefaultAsync(cancellationToken);
        order.PlacedBy = currentUser != null ? currentUser.FullName : "";

        return await PdfService.GenerateOrderSummaryPdfStream(
            new PdfModel<OrderSummaryPdfVm>
            {
                Data = order,
                HeaderImageURL = ContactReportSettings.Value.EngageLogo
            }, cancellationToken
            );
    }
}

public class OrderGenerateSummaryPdfValidator : AbstractValidator<OrderGenerateSummaryPdfCommand>
{
    public OrderGenerateSummaryPdfValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
    }
}