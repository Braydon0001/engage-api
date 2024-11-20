using Engage.Application.Services.ClaimReports.Models;

namespace Engage.Application.Services.ClaimReports.Queries
{
    public class ClaimFNBReportListQuery : GetQuery, IRequest<ListResult<ClaimFNBreportDto>>
    {
        //Required
        public int ClaimClassificationId { get; set; }
        public int EngageRegionId { get; set; }
        public int ClaimPeriodId { get; set; }
        public int[] ClaimIDs { get; set; }

        //Optional
        public int? SupplierId { get; set; }
        public int? ClaimTypeId { get; set; }
        public int? StoreId { get; set; }
        public int? ClaimAccountManagerId { get; set; }
        public int? ClaimManagerId { get; set; }
    }

    public class GetClaimFNBreportListQueryHandler : BaseQueryHandler, IRequestHandler<ClaimFNBReportListQuery, ListResult<ClaimFNBreportDto>>
    {
        public GetClaimFNBreportListQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ListResult<ClaimFNBreportDto>> Handle(ClaimFNBReportListQuery request, CancellationToken cancellationToken)
        {
            var claimRegion = await _context.EngageRegions.SingleOrDefaultAsync(e => e.Id == request.EngageRegionId, cancellationToken);

            var claimPeriod = await _context.ClaimPeriods.Include(c => c.ClaimYear)
                                                         .SingleOrDefaultAsync(c => c.ClaimPeriodId == request.ClaimPeriodId, cancellationToken);

            var claimClassification = await _context.ClaimClassifications.SingleOrDefaultAsync(e => e.ClaimClassificationId == request.ClaimClassificationId);

            var query = _context.Claims.Include(e => e.Store)
                                       .Include(e => e.Store.BankDetails)
                                       .Include(e => e.ClaimSkus)
                                       .Where(e => e.ClaimClassificationId == request.ClaimClassificationId &&
                                                   e.Store.EngageRegionId == request.EngageRegionId &&
                                                  (e.SupplierApprovedDate.Value.Date >= claimPeriod.StartDate.Date && e.SupplierApprovedDate.Value.Date <= claimPeriod.EndDate.Date));

            if (request.ClaimIDs.Length > 0)
            {
                query = query.Where(e => request.ClaimIDs.Contains(e.ClaimId));
            }            

            if (request.SupplierId.HasValue)
            {
                query = query.Where(e => e.SupplierId == request.SupplierId);
            }

            if (request.ClaimTypeId.HasValue)
            {
                query = query.Where(e => e.ClaimTypeId == request.ClaimTypeId);
            }

            if (request.StoreId.HasValue)
            {
                query = query.Where(e => e.StoreId == request.StoreId);
            }

            if (request.ClaimAccountManagerId.HasValue)
            {
                query = query.Where(e => e.ClaimAccountManagerId == request.ClaimAccountManagerId);
            }

            if (request.ClaimManagerId.HasValue)
            {
                query = query.Where(e => e.ClaimManagerId == request.ClaimManagerId);
            }

            var entities = await query.OrderBy(c => c.Store.Name)
                                      .ThenBy(c => c.ClaimNumber)
                                      .ThenBy(c => c.Created)
                                      .ProjectTo<ClaimFNBreportDto>(_mapper.ConfigurationProvider)
                                      .ToListAsync(cancellationToken);

            return new ListResult<ClaimFNBreportDto>(entities);
        }
    }
}
