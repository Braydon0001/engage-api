namespace Engage.Application.Services.EmployeeHealthConditions.Commands;

public class EmployeeHealthConditionInsertCommand : IMapTo<EmployeeHealthCondition>, IRequest<EmployeeHealthCondition>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeHealthConditionInsertCommand, EmployeeHealthCondition>();
    }
}

public class EmployeeHealthConditionInsertHandler : InsertHandler, IRequestHandler<EmployeeHealthConditionInsertCommand, EmployeeHealthCondition>
{
    public EmployeeHealthConditionInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeHealthCondition> Handle(EmployeeHealthConditionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<EmployeeHealthConditionInsertCommand, EmployeeHealthCondition>(command);

        _context.EmployeeHealthConditions.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EmployeeHealthConditionInsertValidator : AbstractValidator<EmployeeHealthConditionInsertCommand>
{
    public EmployeeHealthConditionInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
        RuleFor(e => e.Description).MaximumLength(120);
    }
}