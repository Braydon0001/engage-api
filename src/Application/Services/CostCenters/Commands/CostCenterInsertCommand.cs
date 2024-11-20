namespace Engage.Application.Services.CostCenters.Commands;

public class CostCenterInsertCommand : IMapTo<CostCenter>, IRequest<CostCenter>
{
    public int? CostTypeId { get; init; } = 1; //Default
    public string Name { get; init; }
    public List<int> CostDepartmentIds { get; init; }
    public List<int> CostEmployeeIds { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostCenterInsertCommand, CostCenter>();
    }
}

public record CostCenterInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostCenterInsertCommand, CostCenter>
{
    public async Task<CostCenter> Handle(CostCenterInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CostCenterInsertCommand, CostCenter>(command);

        if (command.CostDepartmentIds != null && command.CostDepartmentIds.Count > 0)
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

        if (command.CostEmployeeIds != null && command.CostEmployeeIds.Count > 0)
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

        Context.CostCenters.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CostCenterInsertValidator : AbstractValidator<CostCenterInsertCommand>
{
    public CostCenterInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}