using Engage.Application.Services.VatPeriods.Models;

namespace Engage.Application.Services.VatPeriods.Queries
{
    public class VatPeriodsQuery : GetQuery, IRequest<ListResult<VatPeriodDto>>
    {
        public int? VatId { get; set; }
    }

    public class VatPeriodsQueryHandler : BaseQueryHandler, IRequestHandler<VatPeriodsQuery, ListResult<VatPeriodDto>>
    {
        public VatPeriodsQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<ListResult<VatPeriodDto>> Handle(VatPeriodsQuery query, CancellationToken cancellationToken)
        {
            var queryable = _context.VatPeriods.AsQueryable().AsNoTracking();

            if (query.VatId.HasValue)
            {
                queryable = queryable.Where(e => e.VatId == query.VatId);
            }

            var entities = await queryable.OrderBy(e => e.VatPeriodId)
                                          .ProjectTo<VatPeriodDto>(_mapper.ConfigurationProvider)
                                          .ToListAsync(cancellationToken);

            return new ListResult<VatPeriodDto>(entities);
        }
    }
}
