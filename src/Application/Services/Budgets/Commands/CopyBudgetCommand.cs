using Engage.Application.Services.Budgets.Models;

namespace Engage.Application.Services.Budgets.Commands;

public class CopyBudgetCommand : BudgetCommand, IRequest<OperationStatus>
{
    // Get from
    public int FromBudgetYearId { get; set; }
    public int FromBudgetVersionId { get; set; }

    // Send to
    public int ToBudgetYearId { get; set; }
    public int ToBudgetVersionId { get; set; }
}

public class CopyBudgetCommandHandler : IRequestHandler<CopyBudgetCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    private readonly IMapper _mapper;


    public CopyBudgetCommandHandler(IAppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OperationStatus> Handle(CopyBudgetCommand command, CancellationToken cancellationToken)
    {
        var entities = await _context.Budgets
            .Where(x => x.BudgetYearId == command.FromBudgetYearId && x.BudgetVersionId == command.FromBudgetVersionId)
            .ProjectTo<BudgetListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var budgets = new List<Budget>();
        foreach (var item in entities)
        {
            var budget = new Budget
            {
                GLAccountId = item.GLAccountId,
                BudgetTypeId = item.BudgetTypeId,
                BudgetYearId = command.ToBudgetYearId,
                BudgetVersionId = command.ToBudgetVersionId,
                BudgetPeriodId = item.BudgetPeriodId,
                Value = item.Value
            };

            budgets.Add(budget);
        }

        _context.Budgets.AddRange(budgets);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        return opStatus;

        //var returnObject = new ListVM<BudgetListDto>
        //{
        //    Data = budgets,
        //    Count = budgets.Count
        //};

        //return new OperationStatus { Exception = false, Status = true, ReturnObject = returnObject };
    }
}
