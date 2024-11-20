namespace Engage.Application.Services.EmployeeJobTitles.Commands;

public class EmployeeJobTitleCreateCommand : EmployeeJobTitleCommand, IRequest<OperationStatus>
{
}

public class EmployeeJobTitleCreateHandler : BaseCreateCommandHandler, IRequestHandler<EmployeeJobTitleCreateCommand, OperationStatus>
{
    public EmployeeJobTitleCreateHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeJobTitleCreateCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<EmployeeJobTitleCreateCommand, EmployeeJobTitle>(command);
        _context.EmployeeJobTitles.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeJobTitleId;
        return opStatus;
    }
}
