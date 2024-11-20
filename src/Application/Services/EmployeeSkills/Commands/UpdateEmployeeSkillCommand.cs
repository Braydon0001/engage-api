namespace Engage.Application.Services.EmployeeSkills.Commands;

public class UpdateEmployeeSkillCommand : EmployeeSkillCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEmployeeSkillCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeSkillCommand, OperationStatus>
{
    public UpdateEmployeeSkillCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(UpdateEmployeeSkillCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeSkills.SingleAsync(x => x.EmployeeSkillId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeSkillId;
        return opStatus;
    }
}
