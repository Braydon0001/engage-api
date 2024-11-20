namespace Engage.Application.Services.EmployeePopiConsents.Commands;

public class UpdateEmployeePopiConsentCommand : IRequest<OperationStatus>
{
    public int EmployeeId { get; set; }

}

//Handlers
public class UpdateEmployeePopiConsentCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeePopiConsentCommand, OperationStatus>
{


    public UpdateEmployeePopiConsentCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(UpdateEmployeePopiConsentCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeePopiConsents.SingleOrDefaultAsync(x => x.EmployeeId == command.EmployeeId, cancellationToken);

        if (entity == null)
        {
            var newEntity = new EmployeePopiConsent
            {
                EmployeeId = command.EmployeeId,
                DateOfConsent = DateTime.Now
            };
            _context.EmployeePopiConsents.Add(newEntity);
        }
        else
        {
            entity.DateOfConsent = DateTime.Now;
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = command.EmployeeId;

        return opStatus;
    }
}

