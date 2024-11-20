using Engage.Application.Services.ClaimSkus.Models;

namespace Engage.Application.Services.ClaimSkus.Queries
{
    public class GetClaimSkusQuery : GetQuery, IRequest<ListResult<ClaimSkuDto>>
    {
        public int ClaimId { get; set; }
    }

    public class GetClaimSkusQueryHandler : BaseQueryHandler, IRequestHandler<GetClaimSkusQuery, ListResult<ClaimSkuDto>>
    {
        private readonly IMediator _mediator;

        public GetClaimSkusQueryHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper)
        {
            _mediator = mediator;
        }

        public async Task<ListResult<ClaimSkuDto>> Handle(GetClaimSkusQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.ClaimSkus.Where(e => e.ClaimId == request.ClaimId)
                                                   .OrderBy(e => e.ClaimSkuId)
                                                   .ProjectTo<ClaimSkuDto>(_mapper.ConfigurationProvider)
                                                   .ToListAsync(cancellationToken);

            return new ListResult<ClaimSkuDto>
            {
                Count = entities.Count,
                Data = entities,
            };
        }


    }
}
