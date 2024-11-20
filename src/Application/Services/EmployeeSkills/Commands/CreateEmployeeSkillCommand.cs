namespace Engage.Application.Services.EmployeeSkills.Commands;

public class CreateEmployeeSkillCommand : EmployeeSkillCommand, IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }
}

public class CreateEmployeeSkillCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEmployeeSkillCommand, OperationStatus>
{
    public CreateEmployeeSkillCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateEmployeeSkillCommand command, CancellationToken cancellationToken)
    {

        var entity = _mapper.Map<CreateEmployeeSkillCommand, EmployeeSkill>(command);
        entity.EmployeeId = command.EmployeeId;
        _context.EmployeeSkills.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeSkillId;
        return opStatus;
    }
}
