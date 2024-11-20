using Engage.Application.Services.BudgetYearVersions.Models;

namespace Engage.Application.Services.BudgetYearVersions.Queries
{
    public class BudgetYearVersionVmQuery : GetByIdQuery, IRequest<BudgetYearVersionVm>
    {
        public int BudgetYearId { get; set; }
        public int BudgetVersionId { get; set; }
    }

    public class BudgetYearVersionVmQueryHandler : BaseQueryHandler, IRequestHandler<BudgetYearVersionVmQuery, BudgetYearVersionVm>
    {
        public BudgetYearVersionVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<BudgetYearVersionVm> Handle(BudgetYearVersionVmQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.BudgetYearVersions.Include(e => e.BudgetYear)
                                                          .Include(e => e.BudgetVersion)
                                                          .SingleOrDefaultAsync(e => e.BudgetYearId == request.BudgetYearId &&
                                                                                     e.BudgetVersionId == request.BudgetVersionId,
                                                                                     cancellationToken);

            if (entity == null)
            {
                return null;
            }

            return _mapper.Map<BudgetYearVersion, BudgetYearVersionVm>(entity);
        }
    }
}
