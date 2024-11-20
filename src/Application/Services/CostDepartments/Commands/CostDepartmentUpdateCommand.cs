namespace Engage.Application.Services.CostDepartments.Commands;

public class CostDepartmentUpdateCommand : IMapTo<CostDepartment>, IRequest<CostDepartment>
{
    public int Id { get; set; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostDepartmentUpdateCommand, CostDepartment>();
    }
}

public record CostDepartmentUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostDepartmentUpdateCommand, CostDepartment>
{
    public async Task<CostDepartment> Handle(CostDepartmentUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CostDepartments.SingleOrDefaultAsync(e => e.CostDepartmentId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCostDepartmentValidator : AbstractValidator<CostDepartmentUpdateCommand>
{
    public UpdateCostDepartmentValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}