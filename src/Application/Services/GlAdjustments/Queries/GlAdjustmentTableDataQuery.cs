using Engage.Application.Services.GLAdjustments.Models;

namespace Engage.Application.Services.GLAdjustments.Queries;

public class GlAdjustmentTableDataQuery : GetQuery, IRequest<ListResult<GlAdjustmentTableDataDto>>
{
    public int YearId { get; set; }
    public int PeriodId { get; set; }
}

public class GetGlAdjustmentsTableDataQueryHandler : BaseQueryHandler, IRequestHandler<GlAdjustmentTableDataQuery, ListResult<GlAdjustmentTableDataDto>>
{
    public GetGlAdjustmentsTableDataQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<ListResult<GlAdjustmentTableDataDto>> Handle(GlAdjustmentTableDataQuery request, CancellationToken cancellationToken)
    {
        var entities = await _context.GLAdjustments
            .Include(e => e.GLAdjustmentType)
            .Include(e => e.Supplier)
            .Where(e => e.BudgetYearId == request.YearId && e.BudgetPeriodId == request.PeriodId)
            .Select(e => new GlAdjustmentTableDataDto
            {
                Id = e.GLAdjustmentId,
                Type = e.GLAdjustmentType.Name,
                GLAdjustmentTypeId = e.GLAdjustmentTypeId,
                ManualAdjustments = "null",
                UpdatedBy = e.LastModifiedBy ?? "null",
                GLCode = e.GLCode.ToString(),
                GLDescription = e.GLDescription,
                TransactionDate = e.TransactionDate,
                DocumentNo = e.DocumentNo,
                NettValue = e.CreditValue + e.DebitValue,
                DebitValue = e.DebitValue,
                CreditValue = e.CreditValue,
                Description = e.Description,
                Invoice = e.Invoice,
                Account = e.Account,
                SupplierName = e.Supplier.Name,
                SupplierId = e.SupplierId,
                BudgetYearId = (int)e.BudgetYearId,
                BudgetPeriodId = (int)e.BudgetPeriodId
            })
            .OrderBy(e => e.Id)
            .ToListAsync(cancellationToken);

        return new ListResult<GlAdjustmentTableDataDto>(entities);
    }
}
