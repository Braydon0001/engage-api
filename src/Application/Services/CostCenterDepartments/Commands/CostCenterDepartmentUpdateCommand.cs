namespace Engage.Application.Services.CostCenterDepartments.Commands;

public class CostCenterDepartmentUpdateCommand : IMapTo<CostCenterDepartment>, IRequest<CostCenterDepartment>
{
    public int Id { get; set; }
    public int CostCenterId { get; init; }
    public int CostDepartmentId { get; init; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<CostCenterDepartmentUpdateCommand, CostCenterDepartment>();
    }
}

public record CostCenterDepartmentUpdateHandler(IAppDbContext Context, IMapper Mapper) : IRequestHandler<CostCenterDepartmentUpdateCommand, CostCenterDepartment>
{
    public async Task<CostCenterDepartment> Handle(CostCenterDepartmentUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await Context.CostCenterDepartments.SingleOrDefaultAsync(e => e.CostCenterDepartmentId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = Mapper.Map(command, entity);

        await Context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateCostCenterDepartmentValidator : AbstractValidator<CostCenterDepartmentUpdateCommand>
{
    public UpdateCostCenterDepartmentValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CostCenterId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.CostDepartmentId).NotEmpty().GreaterThan(0);
    }
}