using Engage.Application.Services.CostCenterEmployees.Commands;
using Engage.Application.Services.Shared.AssignCommands;
using Engage.Application.Services.Users.Commands;
using Finbuckle.MultiTenant.Abstractions;

namespace Engage.Application.Services.Employees.Commands;

public class UpdateEmployeeCommand : EmployeeCommand, IRequest<OperationStatus>
{
    public int Id { get; set; }
}

public class UpdateEmployeeCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeCommand, OperationStatus>
{
    private readonly IMultiTenantContextAccessor<TenantAndSupplierInfo> _multiTenantContextAccessor;

    public UpdateEmployeeCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IMultiTenantContextAccessor<TenantAndSupplierInfo> multiTenantContextAccessor) :
        base(context, mapper, mediator)
    {
        _multiTenantContextAccessor = multiTenantContextAccessor;
    }

    public async Task<OperationStatus> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.Employees.SingleAsync(x => x.EmployeeId == command.Id, cancellationToken);

        if (command.EndDate.HasValue)
        {
            if (command.EndDate.Value.Date < DateTime.Now.Date)
            {
                entity.Disabled = true;
            }
            else
            {
                if (entity.Disabled)
                {
                    entity.Disabled = false;
                }
            }
        }
        else
        {
            if (entity.Disabled)
            {
                entity.Disabled = false;
            }
        }

        if (command.SkipUserCreation == false)
        {
            var user = await _context.Users.Include(u => u.UserGroups)
                                       .ThenInclude(x => x.UserGroup)
                                       .FirstOrDefaultAsync(x => x.Email == entity.EmailAddress1,
                                                            cancellationToken);
            if (user != null)
            {
                var userGroups = user.UserGroups.Where(e => e.UserGroup.Name != "Everyone").Select(u => u.UserGroupId).ToList();
                var updateUserOp = await _mediator.Send(new UpdateUserCommand
                {
                    Id = user.UserId,
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    Email = command.EmailAddress1,
                    SupplierId = _multiTenantContextAccessor.MultiTenantContext.TenantInfo.SupplierId ?? 126,
                    IgnoreOrderProductFilters = false,
                    Groups = userGroups,
                    IsEmployeeEmailUpdate = false,
                }, cancellationToken);
            }
        }

        _mapper.Map(command, entity);

        if (command.EngageDepartmentIds != null)
        {
            await _mediator.Send(new BatchAssignCommand(AssignDesc.DEPT_EMPLOYEE, command.Id, command.EngageDepartmentIds), cancellationToken);
        }

        if (command.EngageRegionIds != null)
        {
            await _mediator.Send(new BatchAssignCommand(AssignDesc.REGION_EMPLOYEE, command.Id, command.EngageRegionIds), cancellationToken);
        }

        if (command.EmployeeJobTitleIds != null)
        {
            await _mediator.Send(new BatchAssignCommand(
                AssignDesc.JOB_TITLE_EMPLOYEE, entity.EmployeeId, command.EmployeeJobTitleIds), cancellationToken);
        }

        if (command.EmployeeDivisionIds != null)
        {
            await _mediator.Send(new BatchAssignCommand(
                AssignDesc.DIVISION_EMPLOYEE, entity.EmployeeId, command.EmployeeDivisionIds), cancellationToken);
        }

        if (command.CostCenterIds != null)
        {
            await _mediator.Send(new CostCenterEmployeeUpdateCommand
            {
                EmployeeId = entity.EmployeeId,
                CostCenterIds = command.CostCenterIds,
                Save = false
            });
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeId;
        return opStatus;
    }
}
