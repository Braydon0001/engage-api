using Engage.Application.Services.OrderSkus.Models;

namespace Engage.Application.Services.OrderSkus.Queries
{
    public class GetOrderSkusByQuantityTypeQuery : GetQuery, IRequest<OrderSkusByQuantityTypeDto>
    {
        public string OrderIds { get; set; }
        public int? OrderId { get; set; }
    }

    public class GetOrderSkusByQuantityTypeQueryHandler : IRequestHandler<GetOrderSkusByQuantityTypeQuery, OrderSkusByQuantityTypeDto>
    {
        private readonly IMediator _mediator;
        private readonly OrderDefaultsOptions _options;

        public GetOrderSkusByQuantityTypeQueryHandler(IMediator mediator, IOptions<OrderDefaultsOptions> options)
        {
            _mediator = mediator;
            _options = options.Value;
        }

        public async Task<OrderSkusByQuantityTypeDto> Handle(GetOrderSkusByQuantityTypeQuery query, CancellationToken cancellationToken)
        {
            var skus = await _mediator.Send(new OrderSkuListQuery
            {
                OrderIds = query.OrderIds,
                OrderId = query.OrderId
            }, cancellationToken);

            var productSkus = skus.Data.Where(e => e.OrderSkuTypeId == _options.SkuTypeId)
                                       .GroupBy(e => e.OrderQuantityTypeName)
                                       .OrderBy(group => group.Key)
                                       .ToDictionary(group => group.Key, group => group.ToList());

            productSkus = productSkus.OrderByDescending(e => e.Value.Sum(d => d.Quantity)).ToDictionary(e => e.Key, e => e.Value);

            var freeTextSkus = skus.Data.Where(e => e.OrderSkuTypeId == _options.DescriptionSkuTypeId)
                                        .ToList();

            return new OrderSkusByQuantityTypeDto
            {
                ProductSkus = productSkus,
                FreeTextSkus = freeTextSkus
            };

        }
    }
}
