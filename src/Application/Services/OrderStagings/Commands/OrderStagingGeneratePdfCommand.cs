using Engage.Application.Services.OrderStagings.Queries;

namespace Engage.Application.Services.OrderStagings.Commands;

public class OrderStagingGeneratePdfCommand : IRequest<MemoryStream>
{
    public int Id { get; set; }
}
public record OrderStagingGeneratePdfHandler(IAppDbContext Context, IMapper Mapper, IPdfService PdfService, IOptions<OrderDefaultsOptions> Options, IOptions<ContactReportSettings> ContactReportSettings) : IRequestHandler<OrderStagingGeneratePdfCommand, MemoryStream>
{
    public async Task<MemoryStream> Handle(OrderStagingGeneratePdfCommand command, CancellationToken cancellationToken)
    {
        var order = await Context.OrderStagings
                           .AsNoTracking()
                           .Where(e => e.OrderStagingId == command.Id)
                           .ProjectTo<OrderStagingPdfDto>(Mapper.ConfigurationProvider)
                           .FirstOrDefaultAsync(cancellationToken);

        var skus = await Context.OrderStagingSkus
                                     .AsNoTracking()
                                     .Where(e => e.OrderStagingId == command.Id)
                                     .ProjectTo<OrderStagingSkuPdfDto>(Mapper.ConfigurationProvider)
                                     .ToListAsync(cancellationToken);

        var orderSkus = skus.GroupBy(e => e.UnitType)
                            .OrderBy(e => e.Key)
                            .ToDictionary(key => key.Key, group => group.ToList());

        order.Skus = orderSkus.OrderByDescending(e => e.Value.Sum(d => d.Quantity)).ToDictionary(e => e.Key, e => e.Value);

        return await PdfService.GenerateOrderStagingSummaryPdfStream(
            new PdfModel<OrderStagingPdfDto>
            {
                Data = order,
                HeaderImageURL = ContactReportSettings.Value.EngageLogo
            }, cancellationToken);
    }
}
