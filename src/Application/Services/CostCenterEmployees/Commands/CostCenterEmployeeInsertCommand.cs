namespace Engage.Application.Services.CostCenterEmployees.Commands;

public class CostCenterEmployeeInsertCommand : IMapTo<CostCenterEmployee>, IRequest<CostCenterEmployee>
{
    public int CostCenterId { get; init; }
    public int EmployeeId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostCenterEmployeeInsertCommand, CostCenterEmployee>();
    }
}

public record CostCenterEmployeeInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostCenterEmployeeInsertCommand, CostCenterEmployee>
{
    public async Task<CostCenterEmployee> Handle(CostCenterEmployeeInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CostCenterEmployeeInsertCommand, CostCenterEmployee>(command);
        
        Context.CostCenterEmployees.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CostCenterEmployeeInsertValidator : AbstractValidator<CostCenterEmployeeInsertCommand>
{
    public CostCenterEmployeeInsertValidator()
    {
        RuleFor(e => e.CostCenterId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
    }
}