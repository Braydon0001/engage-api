namespace Engage.Application.Services.EmployeeJobTitles.Commands;

public class EmployeeJobTitleUpdateCommand : EmployeeJobTitleCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class EmployeeJobTitleUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<EmployeeJobTitleUpdateCommand, OperationStatus>
{
    public EmployeeJobTitleUpdateHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeJobTitleUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeJobTitles.SingleAsync(x => x.EmployeeJobTitleId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeJobTitleId;
        return opStatus;
    }
}
