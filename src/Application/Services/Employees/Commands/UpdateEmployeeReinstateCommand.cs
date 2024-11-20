using Engage.Application.Services.Users.Commands;

namespace Engage.Application.Services.Employees.Commands;

public class UpdateEmployeeReinstateCommand : IRequest<OperationStatus>
{
    public int Id { get; set; }
    public int EmployeeReinstatementReasonId { get; set; }
    public int EmploymentActionId { get; set; }
    public DateTime ReinstatementDate { get; set; }

}
public class UpdateEmployeeReinstateCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeReinstateCommand, OperationStatus>
{
    public UpdateEmployeeReinstateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator)
    {
    }

    public async Task<OperationStatus> Handle(UpdateEmployeeReinstateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees
            .SingleAsync(e => e.EmployeeId == command.Id, cancellationToken);

        entity.EmployeeReinstatementReasonId = command.EmployeeReinstatementReasonId;
        entity.EmploymentActionId = command.EmploymentActionId;
        entity.ReinstatementDate = command.ReinstatementDate;

        entity.EmployeeTerminationReasonId = null;
        entity.EndDate = null;
        entity.Disabled = false;

        var reinstatementHistory = new EmployeeReinstatementHistory
        {
            EmployeeId = command.Id,
            EmployeeReinstatementReasonId = command.EmployeeReinstatementReasonId,
            ReinstatementDate = command.ReinstatementDate,
        };

        _context.EmployeeReinstatementHistories.Add(reinstatementHistory);

        if (entity.UserId.HasValue)
        {
            await _mediator.Send(new RemoveUserCommand { Id = (int)entity.UserId });
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }
}
public class UpdateEmployeeReinstateValidator : AbstractValidator<UpdateEmployeeReinstateCommand>
{
    public UpdateEmployeeReinstateValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(x => x.ReinstatementDate).NotEmpty();
        RuleFor(x => x.EmployeeReinstatementReasonId).NotEmpty().GreaterThan(0);
        RuleFor(x => x.EmploymentActionId).NotEmpty().GreaterThan(0);
    }
}