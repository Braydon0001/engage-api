namespace Engage.Application.Services.CostCenterEmployees.Commands;

public class CostCenterEmployeeUpdateCommand : IRequest<OperationStatus>
{
    public List<int> CostCenterIds { get; init; }
    public int EmployeeId { get; init; }
    public bool Save { get; init; } = true;
}

public record CostCenterEmployeeUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostCenterEmployeeUpdateCommand, OperationStatus>
{
    public async Task<OperationStatus> Handle(CostCenterEmployeeUpdateCommand command, CancellationToken cancellationToken)
    {
        var employeeCostCenters = await Context.CostCenterEmployees.Where(e => e.EmployeeId == command.EmployeeId).ToListAsync(cancellationToken);

        var costCentersToDelete = employeeCostCenters.Where(e => !command.CostCenterIds.Contains(e.EmployeeId)).ToList();

        var costCentersToAdd = command.CostCenterIds.Where(e => !employeeCostCenters.Select(f => f.CostCenterId).ToList().Contains(e)).ToList();

        if (costCentersToDelete.Count > 0)
        {
            Context.CostCenterEmployees.RemoveRange(costCentersToDelete);
        }

        if (costCentersToAdd.Count > 0)
        {
            Context.CostCenterEmployees.AddRange(costCentersToAdd.Select(e => new CostCenterEmployee
            {
                EmployeeId = command.EmployeeId,
                CostCenterId = e
            }));
        }

        OperationStatus status = new();

        if (command.Save)
        {
            status = await Context.SaveChangesAsync(cancellationToken);
        }

        return status;
    }
}

public class UpdateCostCenterEmployeeValidator : AbstractValidator<CostCenterEmployeeUpdateCommand>
{
    public UpdateCostCenterEmployeeValidator()
    {
        RuleFor(e => e.CostCenterIds).NotNull();
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
    }
}