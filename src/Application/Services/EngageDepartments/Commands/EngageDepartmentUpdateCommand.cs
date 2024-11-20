namespace Engage.Application.Services.EngageDepartments.Commands;

public class EngageDepartmentUpdateCommand : EngageDepartmentCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class EngageDepartmentUpdateHandler : BaseUpdateCommandHandler, IRequestHandler<EngageDepartmentUpdateCommand, OperationStatus>
{
    public EngageDepartmentUpdateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(EngageDepartmentUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageDepartments.SingleAsync(x => x.Id == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.Id;
        return opStatus;
    }
}
