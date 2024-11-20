namespace Engage.Application.Services.CostCenters.Commands;

public class CostCenterUpdateCommand : IMapTo<CostCenter>, IRequest<CostCenter>
{
    public int Id { get; set; }
    public int? CostTypeId { get; init; } = 1; //Default
    public string Name { get; init; }
    public List<int> CostDepartmentIds { get; init; }
    public List<int> CostEmployeeIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostCenterUpdateCommand, CostCenter>();
    }
}

public record CostCenterUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostCenterUpdateCommand, CostCenter>
{
    public async Task<CostCenter> Handle(CostCenterUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CostCenters.SingleOrDefaultAsync(e => e.CostCenterId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        if (command.CostDepartmentIds != null && command.CostDepartmentIds.Count > 0)
        {
            var existingCostCenterDepartments = await Context.CostCenterDepartments.Where(e => e.CostCenterId == command.Id)
                                                                                   .ToListAsync(cancellationToken);

            var costDepartmentsToRemove = existingCostCenterDepartments.Where(e => !command.CostDepartmentIds.Contains(e.CostDepartmentId))
                                                                       .ToList();
            Context.CostCenterDepartments.RemoveRange(costDepartmentsToRemove);

            var costDepartmentsToAdd = command.CostDepartmentIds.Where(e => !existingCostCenterDepartments.Select(d => d.CostDepartmentId).Contains(e))
                                                                .ToList();

            if (costDepartmentsToAdd.Count > 0)
            {
                foreach (var costDepartmentId in command.CostDepartmentIds)
                {
                    Context.CostCenterDepartments.Add(new CostCenterDepartment
                    {
                        CostCenter = entity,
                        CostDepartmentId = costDepartmentId
                    });
                }
            }
        }

        if (command.CostEmployeeIds != null && command.CostEmployeeIds.Count > 0)
        {
            var existingCostCenterEmployees = await Context.CostCenterEmployees.Where(e => e.CostCenterId == command.Id)
                                                                               .ToListAsync(cancellationToken);

            var costEmployeesToRemove = existingCostCenterEmployees.Where(e => !command.CostEmployeeIds.Contains(e.EmployeeId))
                                                                   .ToList();
            Context.CostCenterEmployees.RemoveRange(costEmployeesToRemove);

            var costEmployeesToAdd = command.CostEmployeeIds.Where(e => !existingCostCenterEmployees.Select(d => d.EmployeeId).Contains(e))
                                                            .ToList();

            if (costEmployeesToAdd.Count > 0)
            {
                foreach (var costEmployeeId in command.CostEmployeeIds)
                {
                    Context.CostCenterEmployees.Add(new CostCenterEmployee
                    {
                        CostCenter = entity,
                        EmployeeId = costEmployeeId
                    });
                }
            }
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCostCenterValidator : AbstractValidator<CostCenterUpdateCommand>
{
    public UpdateCostCenterValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}