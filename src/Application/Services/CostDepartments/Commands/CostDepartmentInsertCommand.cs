namespace Engage.Application.Services.CostDepartments.Commands;

public class CostDepartmentInsertCommand : IMapTo<CostDepartment>, IRequest<CostDepartment>
{
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostDepartmentInsertCommand, CostDepartment>();
    }
}

public record CostDepartmentInsertHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostDepartmentInsertCommand, CostDepartment>
{
    public async Task<CostDepartment> Handle(CostDepartmentInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = Mapper.Map<CostDepartmentInsertCommand, CostDepartment>(command);
        
        Context.CostDepartments.Add(entity);

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class CostDepartmentInsertValidator : AbstractValidator<CostDepartmentInsertCommand>
{
    public CostDepartmentInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}