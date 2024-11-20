using Engage.Application.Services.Shared.AssignCommands;
namespace Engage.Application.Services.Employees.Commands;

using Engage.Application.Services.CostCenterEmployees.Commands;
using Engage.Application.Services.Users.Commands;
using Finbuckle.MultiTenant.Abstractions;

public class CreateEmployeeCommand : IMapTo<Employee>, IRequest<OperationStatus>
{
    public int EmployeeStateId { get; set; } = 1;
    public int EmployeePassportNationalityId { get; set; } = 1;
    public int EmployeePersonTypeId { get; set; } = 1;
    public int EmployeeSDLExemptionId { get; set; } = 1;
    public int EmployeeTaxStatusId { get; set; } = 1;
    public int EmployeeUIFExemptionId { get; set; } = 1;
    public int EmployeeStandardIndustryGroupCodeId { get; set; } = 1;
    public int EmployeeStandardIndustryCodeId { get; set; } = 1;
    public int EmployeeIdentificationTypeId { get; set; } = 1;
    public int EmployeeCitzenshipId { get; set; } = 1;
    public int? EmployeeDisabledTypeId { get; set; } = 1;
    public int? EmployeeLanguageId { get; set; } = 1;
    public int? EmployeeNationalityId { get; set; } = 1;
    public int? MaritalStatusId { get; set; } = 1;
    public int? UniformSizeId { get; set; } = 1;
    public int? NextOfKinTypeId { get; set; } = 1;
    public string Code { get; set; }
    public int? EmployeeTitleId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int? EmployeeRaceId { get; set; }
    public string EmailAddress1 { get; set; }
    public int? EmployeeGenderId { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<int> EngageDepartmentIds { get; set; }
    public List<int> EngageRegionIds { get; set; }
    public int? EngageSubRegionId { get; set; }
    public List<int> CostCenterIds { get; set; }
    public List<int> EmployeeJobTitleIds { get; set; }
    public List<int> EmployeeDivisionIds { get; set; }
    public int? EmployeeTerminationReasonId { get; set; }
    public int? ManagerId { get; set; }
    public int? LeaveManagerId { get; set; }
    public bool SkipUserCreation { get; set; } = false;
    public int? EmployeeTypeId { get; set; } = 1;
    public int? UserId { get; set; }
    public int? CostCenterManagerId { get; set; }
    public int? EmployeeJobTitleTimeId { get; set; }
    public int? EmployeeJobTitleTypeId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateEmployeeCommand, Employee>()
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstName.ToUpper()))
            .ForMember(d => d.LastName, opt => opt.MapFrom(s => s.LastName.ToUpper()))
            .ForMember(dest => dest.EmployeeTerminationReasonId, opt => opt.MapFrom(
                       src => src.EmployeeTerminationReasonId.HasValue && src.EmployeeTerminationReasonId == 0 ? null : src.EmployeeTerminationReasonId))
            .ForMember(dest => dest.ManagerId, opt => opt.MapFrom(
                       src => src.ManagerId.HasValue && src.ManagerId == 0 ? null : src.ManagerId))
            .ForMember(dest => dest.LeaveManagerId, opt => opt.MapFrom(
                       src => src.LeaveManagerId.HasValue && src.LeaveManagerId == 0 ? null : src.LeaveManagerId))
            .ForMember(dest => dest.CostCenterManagerId, opt => opt.MapFrom(
                       src => src.CostCenterManagerId.HasValue && src.CostCenterManagerId == 0 ? null : src.CostCenterManagerId));
    }
}

public class CreateEmployeeCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEmployeeCommand, OperationStatus>
{
    private readonly IMultiTenantContextAccessor<TenantAndSupplierInfo> _multiTenantContextAccessor;

    public CreateEmployeeCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator, IMultiTenantContextAccessor<TenantAndSupplierInfo> multiTenantContextAccessor) :
        base(context, mapper, mediator)
    {
        _multiTenantContextAccessor = multiTenantContextAccessor;
    }

    public async Task<OperationStatus> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        var existingCode = await _context.Employees.AnyAsync(x => x.Code == command.Code, cancellationToken);
        if (existingCode)
        {
            throw new Exception("Employee code already exists.");
        }
        var entity = _mapper.Map<CreateEmployeeCommand, Employee>(command);
        entity.GroupStartDate = command.StartingDate;
        var stakeholder = new Stakeholder
        {
            StakeholderType = StakeholderTypes.Employee,
            Created = DateTime.Now,
        };
        entity.Stakeholder = stakeholder;

        var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == entity.EmailAddress1, cancellationToken);
        if (user != null)
        {
            entity.UserId = user.UserId;
        }
        else if (user == null && command.SkipUserCreation == false)
        {
            List<int> userGroups = new();

            if (command.EmployeeJobTitleIds != null)
            {
                foreach (var jobTitleId in command.EmployeeJobTitleIds)
                {
                    var groupsList = await _context.EmployeeJobTitleUserGroups.Where(x => x.EmployeeJobTitleId == jobTitleId)
                                                                              .Select(x => x.UserGroupId)
                                                                              .ToListAsync(cancellationToken);

                    if (groupsList != null)
                    {
                        userGroups.AddRange(groupsList);
                    }
                }

                userGroups = userGroups.Distinct().ToList();
            }

            var createUserOp = await _mediator.Send(new CreateUserCommand
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.EmailAddress1,
                SupplierId = _multiTenantContextAccessor.MultiTenantContext.TenantInfo.SupplierId ?? 126,
                IgnoreOrderProductFilters = false,
                Groups = userGroups,
                CreateEmployee = false
            }, cancellationToken);

            if (createUserOp.Status)
            {
                entity.UserId = (int)createUserOp.OperationId;
            }
        }

        _context.Employees.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);

        if (opStatus.Status == true)
        {
            opStatus.OperationId = entity.EmployeeId;

            if (command.EngageDepartmentIds != null)
            {
                await _mediator.Send(new BatchAssignCommand(
                  AssignDesc.DEPT_EMPLOYEE, entity.EmployeeId, command.EngageDepartmentIds), cancellationToken);
            }

            if (command.EngageRegionIds != null)
            {
                await _mediator.Send(new BatchAssignCommand(
                    AssignDesc.REGION_EMPLOYEE, entity.EmployeeId, command.EngageRegionIds), cancellationToken);
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

            stakeholder.EmployeeId = entity.EmployeeId;

            await _context.SaveChangesAsync(cancellationToken);
        }

        return opStatus;
    }
}

public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeValidator()
    {
        RuleFor(x => x.Code).MaximumLength(30).NotEmpty();
        RuleFor(x => x.EmployeeTitleId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.FirstName).MaximumLength(120).NotEmpty();
        RuleFor(x => x.LastName).MaximumLength(120).NotEmpty();
        RuleFor(x => x.DateOfBirth).NotEmpty();
        RuleFor(x => x.EmployeeRaceId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EmployeeGenderId).GreaterThan(0).NotEmpty();
        RuleFor(x => x.EmailAddress1).MaximumLength(100);
    }
}