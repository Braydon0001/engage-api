namespace Engage.Application.Services.CostSubDepartments.Commands;

public class CostSubDepartmentInsertCommand : IMapTo<CostSubDepartment>, IRequest<CostSubDepartment>
{
    public int CostDepartmentId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostSubDepartmentInsertCommand, CostSubDepartment>();
    }
}

public record CostSubDepartmentInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostSubDepartmentInsertCommand, CostSubDepartment>
{
    public async Task<CostSubDepartment> Handle(CostSubDepartmentInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CostSubDepartmentInsertCommand, CostSubDepartment>(command);
        
        Context.CostSubDepartments.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CostSubDepartmentInsertValidator : AbstractValidator<CostSubDepartmentInsertCommand>
{
    public CostSubDepartmentInsertValidator()
    {
        RuleFor(e => e.CostDepartmentId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}