using Engage.Application.Contracts;
using Engage.Application.Services.Employees.Queries;
using Engage.Application.Services.Users.Commands;
using MassTransit;

namespace Engage.Application.Services.Employees.Commands;

public class UpdateEmployeeTerminateCommand : EmployeeCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }

}
public class UpdateEmployeeTerminateCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeTerminateCommand, OperationStatus>, IConsumer<TerminationEmail>
{
    private readonly FeatureSwitchOptions _featureSwitch;
    private readonly IUserService _user;
    private new readonly IMediator _mediator;
    private readonly IBus _bus;
    public UpdateEmployeeTerminateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IOptions<FeatureSwitchOptions> featureSwitch, IUserService user, IBus bus) : base(context, mapper, mediator)
    {
        _featureSwitch = featureSwitch.Value;
        _user = user;
        _mediator = mediator;
        _bus = bus;
    }

    public async Task<OperationStatus> Handle(UpdateEmployeeTerminateCommand request, CancellationToken cancellationToken)
    {
        var employeeAssets = await _mediator.Send(new EmployeeAssetsVMQuery { Id = request.Id });

        if (employeeAssets.HasAssets)
        {
            throw new Exception("Cannot terminate employee with assets still assigned");
        }

        var entity = await _context.Employees
            .Include(e => e.EmployeeStores)
            .Include(e => e.Manager)
            .Include(e => e.LeaveManager)
            .SingleAsync(e => e.EmployeeId == request.Id, cancellationToken);

        var employeeVehicles = await _context.EmployeeVehicles.Where(e => e.AssetOwnerId == (int)AssetOwnerId.Peronal && e.EmployeeId == request.Id)
                                                              .ToListAsync(cancellationToken);

        if (employeeVehicles.Count > 0)
        {
            foreach (var vehicles in employeeVehicles)
            {
                vehicles.Disabled = true;
            }
        }

        var payrollPeriod = await _context.PayrollPeriods.SingleOrDefaultAsync(e
            => DateTime.Now.Date >= e.StartDate.Date && DateTime.Now.Date <= e.EndDate.Date, cancellationToken);
        if (payrollPeriod == null)
        {
            throw new Exception("No Payroll Period found");
        }

        var terminationHistory = new EmployeeTerminationHistory
        {
            EmployeeId = request.Id,
            EmployeeTerminationReasonId = (int)request.EmployeeTerminationReasonId,
            TerminationDate = request.EndDate.Value.Date,
        };

        _context.EmployeeTerminationHistories.Add(terminationHistory);

        _mapper.Map(request, entity);
        entity.PayrollPeriodId = payrollPeriod.PayrollPeriodId;

        entity.IsEncashLeave = true;
        entity.Disabled = true;

        if (entity.EmployeeStores.Any())
        {
            //Save call cycles in Archives
            foreach (var employeeStore in entity.EmployeeStores)
            {
                var employeeStoreArchive = new EmployeeStoreArchive
                {
                    EmployeeId = entity.EmployeeId,
                    EngageDepartmentCategoryId = employeeStore.EngageDepartmentCategoryId,
                    EngageSubGroupId = employeeStore.EngageSubGroupId,
                    FrequencyTypeId = employeeStore.FrequencyTypeId,
                    Note = employeeStore.Note,
                    StoreId = employeeStore.StoreId,
                    Frequency = employeeStore.Frequency,
                };

                _context.EmployeeStoreArchives.Add(employeeStoreArchive);
            }
            //Delete call cycles
            _context.EmployeeStores.RemoveRange(entity.EmployeeStores);
        }

        var employeeWorkRoles = await _context.EmployeeWorkRoles.Where(e => e.EmployeeId == entity.EmployeeId
                                                                        && !e.EndDate.HasValue)
                                                                .ToListAsync(cancellationToken);

        foreach (var workRole in employeeWorkRoles)
        {
            workRole.EndDate = request.EndDate;
            workRole.StatusId = (int)WorkRoleStatusId.Ended;
            workRole.Disabled = true;
        }

        if (entity.UserId.HasValue)
        {
            await _mediator.Send(new RemoveUserCommand { Id = (int)entity.UserId });
        }

        if (_featureSwitch.IsEmailEmployeeTermination)
        {
            //send email
            var terminatedBy = await _context.Users.SingleOrDefaultAsync(e => e.Email == _user.UserName);
            var userName = terminatedBy.FullName ?? _user.UserName;
            var terminationReason = await _context.EmployeeTerminationReasons.SingleOrDefaultAsync(e => e.Id == entity.EmployeeTerminationReasonId);
            await _bus.Publish(new TerminationEmail
            {
                EmployeeId = entity.EmployeeId,
                ManagerName = entity.Manager.FirstName,
                ManagerEmail = entity.Manager.EmailAddress1 ?? entity.Manager.EmailAddress2,
                LeaveManagerEmail = entity.LeaveManager.EmailAddress1 ?? entity.LeaveManager.EmailAddress2,
                TerminationReason = terminationReason.Name,
                EmployeeName = $"{entity.FirstName} {entity.LastName}",
                EmployeeCode = entity.Code,
                EndDate = entity.EndDate.Value,
                TerminatedBy = userName
            }, cancellationToken);
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        return opStatus;
    }

    public async Task Consume(ConsumeContext<TerminationEmail> context)
    {
        await _mediator.Send(new UpdateEmployeeTerminationEmailCommand
        {
            EmployeeId = context.Message.EmployeeId,
            ManagerName = context.Message.ManagerName,
            ManagerEmail = context.Message.ManagerEmail,
            LeaveManagerEmail = context.Message.LeaveManagerEmail,
            TerminationReason = context.Message.TerminationReason,
            EmployeeName = context.Message.EmployeeName,
            EmployeeCode = context.Message.EmployeeCode,
            EndDate = context.Message.EndDate,
            TerminatedBy = context.Message.TerminatedBy
        });
    }
}
public class UpdateEmployeeTerminateValidator : AbstractValidator<UpdateEmployeeTerminateCommand>
{
    public UpdateEmployeeTerminateValidator()
    {
        RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        RuleFor(x => x.EndDate).NotEmpty();
        RuleFor(x => x.EmployeeTerminationReasonId).NotEmpty().GreaterThan(0);
    }
}