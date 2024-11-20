namespace Engage.Application.Services.EmployeeDivisions.Commands;

public class EmployeeDivisionUpdateCommand : IMapTo<EmployeeDivision>, IRequest<EmployeeDivision>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsRihCallCycles { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeDivisionUpdateCommand, EmployeeDivision>();
    }
}

public class EmployeeDivisionUpdateHandler : UpdateHandler, IRequestHandler<EmployeeDivisionUpdateCommand, EmployeeDivision>
{
    public EmployeeDivisionUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EmployeeDivision> Handle(EmployeeDivisionUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeDivisions.SingleOrDefaultAsync(e => e.EmployeeDivisionId == command.Id, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        var mappedEntity = _mapper.Map(command, entity);

        await _context.SaveChangesAsync(cancellationToken);

        return mappedEntity;
    }
}

public class UpdateEmployeeDivisionValidator : AbstractValidator<EmployeeDivisionUpdateCommand>
{
    public UpdateEmployeeDivisionValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(e => e.Name).NotEmpty().MaximumLength(120);
        RuleFor(e => e.Description).MaximumLength(120);
    }
}