namespace Engage.Application.Services.CostSubDepartments.Commands;

public class CostSubDepartmentUpdateCommand : IMapTo<CostSubDepartment>, IRequest<CostSubDepartment>
{
    public int Id { get; set; }
    public int CostDepartmentId { get; init; }
    public string Name { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostSubDepartmentUpdateCommand, CostSubDepartment>();
    }
}

public record CostSubDepartmentUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostSubDepartmentUpdateCommand, CostSubDepartment>
{
    public async Task<CostSubDepartment> Handle(CostSubDepartmentUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CostSubDepartments.SingleOrDefaultAsync(e => e.CostSubDepartmentId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCostSubDepartmentValidator : AbstractValidator<CostSubDepartmentUpdateCommand>
{
    public UpdateCostSubDepartmentValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CostDepartmentId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(100);
    }
}