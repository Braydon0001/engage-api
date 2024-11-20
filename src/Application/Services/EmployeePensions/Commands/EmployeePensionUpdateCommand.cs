// auto-generated
namespace Engage.Application.Services.EmployeePensions.Commands;

public class EmployeePensionUpdateCommand : IMapTo<EmployeePension>, IRequest<EmployeePension>
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int EmployeePensionSchemeId { get; set; }
    public int EmployeePensionCategoryId { get; set; }
    public int EmployeePensionContributionPercentageId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeePensionUpdateCommand, EmployeePension>();
    }
}

public class EmployeePensionUpdateHandler : UpdateHandler, IRequestHandler<EmployeePensionUpdateCommand, EmployeePension>
{
    public EmployeePensionUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeePension> Handle(EmployeePensionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeePensions.SingleOrDefaultAsync(e => e.EmployeePensionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEmployeePensionValidator : AbstractValidator<EmployeePensionUpdateCommand>
{
    public UpdateEmployeePensionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeePensionSchemeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeePensionCategoryId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeePensionContributionPercentageId).NotEmpty().GreaterThan(0);
    }
}