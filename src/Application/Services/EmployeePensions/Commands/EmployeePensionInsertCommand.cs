// auto-generated

namespace Engage.Application.Services.EmployeePensions.Commands;

public class EmployeePensionInsertCommand : IMapTo<EmployeePension>, IRequest<EmployeePension>
{
    public int EmployeeId { get; set; }
    public int EmployeePensionSchemeId { get; set; }
    public int EmployeePensionCategoryId { get; set; }
    public int EmployeePensionContributionPercentageId { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeePensionInsertCommand, EmployeePension>();
    }
}

public class EmployeePensionInsertHandler : InsertHandler, IRequestHandler<EmployeePensionInsertCommand, EmployeePension>
{
    public EmployeePensionInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeePension> Handle(EmployeePensionInsertCommand command, CancellationToken cancellationToken)
    {
        var existingPension = await _context.EmployeePensions
                                            .Where(s => s.EmployeeId == command.EmployeeId && s.Disabled == false)
                                            .FirstOrDefaultAsync(cancellationToken);

        if (existingPension != null)
        {
            throw new Exception("Employee Already has Pension. Please update the existing one.");
        }

        var entity = _mapper.Map<EmployeePensionInsertCommand, EmployeePension>(command);

        _context.EmployeePensions.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EmployeePensionInsertValidator : AbstractValidator<EmployeePensionInsertCommand>
{
    public EmployeePensionInsertValidator()
    {
        RuleFor(e => e.EmployeeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeePensionSchemeId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeePensionCategoryId).NotEmpty().GreaterThan(0);
        RuleFor(e => e.EmployeePensionContributionPercentageId).NotEmpty().GreaterThan(0);
    }
}