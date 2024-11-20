namespace Engage.Application.Services.CostCenterDepartments.Commands;

public class CostCenterDepartmentInsertCommand : IMapTo<CostCenterDepartment>, IRequest<CostCenterDepartment>
{
    public int CostCenterId { get; init; }
    public int CostDepartmentId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostCenterDepartmentInsertCommand, CostCenterDepartment>();
    }
}

public record CostCenterDepartmentInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostCenterDepartmentInsertCommand, CostCenterDepartment>
{
    public async Task<CostCenterDepartment> Handle(CostCenterDepartmentInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CostCenterDepartmentInsertCommand, CostCenterDepartment>(command);
        
        Context.CostCenterDepartments.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CostCenterDepartmentInsertValidator : AbstractValidator<CostCenterDepartmentInsertCommand>
{
    public CostCenterDepartmentInsertValidator()
    {
        RuleFor(e => e.CostCenterId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CostDepartmentId).NotEmpty().GreaterThan(0);
    }
}