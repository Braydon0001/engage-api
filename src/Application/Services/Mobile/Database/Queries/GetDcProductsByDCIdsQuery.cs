using Engage.Application.Services.Mobile.Database.Models;

namespace Engage.Application.Services.Mobile.Database.Queries
{
    public class GetDcProductsByDCIdsQuery : IRequest<List<DCProductDto>>
    {
        public List<int> DcIds { get; set; }
    }

    public class GetDcProductsByDCIdsQueryHandler : BaseQueryHandler, IRequestHandler<GetDcProductsByDCIdsQuery, List<DCProductDto>>
    {
        IMediator _mediator;
        public GetDcProductsByDCIdsQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
        {
            _mediator = mediator;
        }

        public async Task<List<DCProductDto>> Handle(GetDcProductsByDCIdsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.DCProducts
                .Where(p => 
                    p.Disabled == false && 
                    p.Deleted == false &&
                    request.DcIds.Contains(p.DistributionCenterId))
                .ProjectTo<DCProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return products;
        }
    }
}
