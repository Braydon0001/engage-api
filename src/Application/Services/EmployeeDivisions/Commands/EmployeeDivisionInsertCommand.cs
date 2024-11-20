namespace Engage.Application.Services.EmployeeDivisions.Commands;

public class EmployeeDivisionInsertCommand : IMapTo<EmployeeDivision>, IRequest<EmployeeDivision>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsRihCallCycles { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeDivisionInsertCommand, EmployeeDivision>();
    }
}

public class EmployeeDivisionInsertHandler : InsertHandler, IRequestHandler<EmployeeDivisionInsertCommand, EmployeeDivision>
{
    public EmployeeDivisionInsertHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeDivision> Handle(EmployeeDivisionInsertCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<EmployeeDivisionInsertCommand, EmployeeDivision>(command);

        _context.EmployeeDivisions.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EmployeeDivisionInsertValidator : AbstractValidator<EmployeeDivisionInsertCommand>
{
    public EmployeeDivisionInsertValidator()
    {
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
        RuleFor(e => e.Description).MaximumLength(120);
    }
}