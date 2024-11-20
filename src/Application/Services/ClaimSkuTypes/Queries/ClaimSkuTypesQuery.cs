using Engage.Application.Services.ClaimSkuTypes.Models;

namespace Engage.Application.Services.ClaimSkuTypes.Queries
{
    public class ClaimSkuTypesQuery : GetQuery, IRequest<ListResult<ClaimSkuTypeDto>>
    {
    }

    public class ClaimSkuTypesQueryHandler : BaseQueryHandler, IRequestHandler<ClaimSkuTypesQuery, ListResult<ClaimSkuTypeDto>>
    {
        public ClaimSkuTypesQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { 
        }

        public async Task<ListResult<ClaimSkuTypeDto>> Handle(ClaimSkuTypesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.ClaimSkuTypes.OrderBy(e => e.ClaimSkuTypeId)
                                                       .ProjectTo<ClaimSkuTypeDto>(_mapper.ConfigurationProvider)
                                                       .ToListAsync(cancellationToken);

            return new ListResult<ClaimSkuTypeDto>(entities);
        }
    } 
}
