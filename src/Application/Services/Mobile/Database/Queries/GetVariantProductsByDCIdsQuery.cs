using Engage.Application.Services.Mobile.Database.Models;

namespace Engage.Application.Services.Mobile.Database.Queries
{
    public class GetVariantProductsByDCIdsQuery : IRequest<List<EngageVariantProductDto>>
    {
        public List<int> DcIds { get; set; }
    }

    public class GetVariantProductsByDCIdsQueryHandler : BaseQueryHandler, IRequestHandler<GetVariantProductsByDCIdsQuery, List<EngageVariantProductDto>>
    {
        IMediator _mediator;
        public GetVariantProductsByDCIdsQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
        {
            _mediator = mediator;
        }

        public async Task<List<EngageVariantProductDto>> Handle(GetVariantProductsByDCIdsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.EngageVariantProducts
                .Where(e => e.DCProducts.Any(d =>
                    d.Deleted == false &&
                    d.Disabled == false &&
                    request.DcIds.Contains(d.DistributionCenterId)))
                .ProjectTo<EngageVariantProductDto>(_mapper.ConfigurationProvider)
                .Distinct()
                .ToListAsync(cancellationToken);

            return products;
        }
    }
}
