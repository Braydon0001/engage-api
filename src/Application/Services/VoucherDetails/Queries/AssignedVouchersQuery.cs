using Engage.Application.Services.VoucherDetails.Models;

namespace Engage.Application.Services.VoucherDetails.Queries
{
    public class AssignedVouchersQuery : GetQuery, IRequest<ListResult<VoucherDetailDto>>
    {
        public int? EngageRegionId { get; set; }
    }

    public class AssignedVouchersQueryHandler : BaseQueryHandler, IRequestHandler<AssignedVouchersQuery, ListResult<VoucherDetailDto>>
    {
        public AssignedVouchersQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
        public async Task<ListResult<VoucherDetailDto>> Handle(AssignedVouchersQuery request, CancellationToken cancellationToken)
        {
            var query = _context.VoucherDetails.Where(e => e.VoucherDetailStatusId == (int)VoucherDetailStatusId.Assigned).AsQueryable();

            if (request.EngageRegionId.HasValue)
            {
                query = query
                            .Where(e => e.Voucher.EngageRegionId == request.EngageRegionId.Value);

            }

            var entities = await query.OrderBy(e => e.VoucherDetailId)
                                      .ProjectTo<VoucherDetailDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

            return new ListResult<VoucherDetailDto>
            {
                Count = entities.Count,
                Data = entities,
            };
        }
    }
}
