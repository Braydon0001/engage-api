namespace Engage.Application.Services.EmployeeHealthConditions.Commands;

public class EmployeeHealthConditionUpdateCommand : IMapTo<EmployeeHealthCondition>, IRequest<EmployeeHealthCondition>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeHealthConditionUpdateCommand, EmployeeHealthCondition>();
    }
}

public class EmployeeHealthConditionUpdateHandler : UpdateHandler, IRequestHandler<EmployeeHealthConditionUpdateCommand, EmployeeHealthCondition>
{
    public EmployeeHealthConditionUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeHealthCondition> Handle(EmployeeHealthConditionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeHealthConditions.SingleOrDefaultAsync(e => e.EmployeeHealthConditionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEmployeeHealthConditionValidator : AbstractValidator<EmployeeHealthConditionUpdateCommand>
{
    public UpdateEmployeeHealthConditionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
        RuleFor(e => e.Description).MaximumLength(120);
    }
}