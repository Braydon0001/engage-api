namespace Engage.Application.Services.EngageDepartments.Commands;

public class EngageDepartmentCreateCommand : EngageDepartmentCommand, IRequest<OperationStatus>
{
}

public class EngageDepartmentCreateHandler : BaseCreateCommandHandler, IRequestHandler<EngageDepartmentCreateCommand, OperationStatus>
{
    public EngageDepartmentCreateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(EngageDepartmentCreateCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<EngageDepartmentCreateCommand, EngageDepartment>(command);
        _context.EngageDepartments.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.Id;
        return opStatus;
    }
}
