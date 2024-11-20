using Engage.Application.Services.Shared.AssignCommands;
using Engage.Application.Targetings;

namespace Engage.Application.Services.SurveyEmployees
{
    // Commands

    public class CreateSurveyEmployeesCommand : IRequest<OperationStatus>
    {
        public int SurveyId { get; set; }
        public List<int> Employees { get; set; }
    }

    public class CreateSurveyEmployeesWithCriteriaCommand : EmployeeTargetingCommand, IRequest<OperationStatus>
    {
        public int SurveyId { get; set; }
    }

    public class DeleteSurveyEmployeeCommand : IRequest<OperationStatus>
    {
        public int SurveyId { get; set; }
        public int EmployeeId { get; set; }
    }

    public class BatchDeleteSurveyEmployeeCommand : IRequest<OperationStatus>
    {
        public int SurveyId { get; set; }
    }

    // Handlers

    public class CreateSurveyEmployeesHandler : BaseCreateCommandHandler, IRequestHandler<CreateSurveyEmployeesCommand, OperationStatus>
    {
        public CreateSurveyEmployeesHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<OperationStatus> Handle(CreateSurveyEmployeesCommand command, CancellationToken cancellationToken)
        {
            var SurveyEmployees = command.Employees.Select(employeeId => new SurveyEmployee
            {
                SurveyId = command.SurveyId,
                EmployeeId = employeeId
            });

            _context.SurveyEmployees.AddRange(SurveyEmployees);

            return OperationStatus.EnsureTrue(await _context.SaveChangesAsync(cancellationToken));
        }
    }

    public class CreateSurveyEmployeesWithCriteriaHandler : BaseCreateCommandHandler, IRequestHandler<CreateSurveyEmployeesWithCriteriaCommand, OperationStatus>
    {
        private readonly ITargetingService _targeting;

        public CreateSurveyEmployeesWithCriteriaHandler(IAppDbContext context, IMapper mapper, ITargetingService targeting) : base(context, mapper)
        {
            _targeting = targeting;
        }

        public async Task<OperationStatus> Handle(CreateSurveyEmployeesWithCriteriaCommand command, CancellationToken cancellationToken)
        {
            // Add Targeting
            var targeting = new Targeting
            {
                Criteria = await _targeting.SerializeEmployeeCriteria(command, cancellationToken)
            };

            _context.Targetings.Add(targeting);

            await _context.SaveChangesAsync(cancellationToken);

            // Ignore employees that have already been added
            var existingEmployees = await _context.SurveyEmployees.Where(e => e.SurveyId == command.SurveyId)
                                                                  .Select(e => e.EmployeeId)
                                                                  .ToListAsync(cancellationToken);

            var employees = _context.Employees.Include(e => e.EmployeeJobTitles).Where(e => !existingEmployees.Contains(e.EmployeeId));

            if (command.JobTitles != null && command.JobTitles.Count > 0)
            {
                employees = employees.Where(e => e.EmployeeJobTitles.Any(jt => command.JobTitles.Contains(jt.EmployeeJobTitleId)));
            }

            if (command.EngageDepartments != null && command.EngageDepartments.Count > 0)
            {
                var employeeIds = await _context.EmployeeDepartments.Where(e => command.EngageDepartments.Contains(e.EngageDepartmentId))
                                                                    .Select(e => e.EmployeeId)
                                                                    .ToListAsync(cancellationToken);
                employees = employees.Where(e => employeeIds.Contains(e.EmployeeId));
            }

            var surveyEmployees = await employees.Select(e => new SurveyEmployee
            {
                SurveyId = command.SurveyId,
                EmployeeId = e.EmployeeId,
                TargetingId = targeting.TargetingId
            })
                                                 .Distinct()
                                                 .ToListAsync(cancellationToken);

            _context.SurveyEmployees.AddRange(surveyEmployees);

            return OperationStatus.EnsureTrue(await _context.SaveChangesAsync(cancellationToken));
        }
    }

    public class DeleteSurveyEmployeeHandler : BaseCreateCommandHandler, IRequestHandler<DeleteSurveyEmployeeCommand, OperationStatus>
    {
        public DeleteSurveyEmployeeHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : base(context, mapper, mediator) { }

        public async Task<OperationStatus> Handle(DeleteSurveyEmployeeCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(new UnassignCommand(AssignDesc.EMPLOYEE_SURVEY, command.SurveyId, command.EmployeeId));

            return new OperationStatus()
            {
                Status = true,
                OperationId = command.SurveyId
            };
        }
    }

    public class BatchDeleteSurveyEmployeeHandler : IRequestHandler<BatchDeleteSurveyEmployeeCommand, OperationStatus>
    {
        private readonly IAppDbContext _context;


        public BatchDeleteSurveyEmployeeHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<OperationStatus> Handle(BatchDeleteSurveyEmployeeCommand command, CancellationToken cancellationToken)
        {
            var entities = await _context.SurveyEmployees.Where(e => e.SurveyId == command.SurveyId)
                                                         .ToListAsync(cancellationToken);

            _context.SurveyEmployees.RemoveRange(entities);

            await _context.SaveChangesAsync(cancellationToken);

            return new OperationStatus
            {
                Status = true,
                OperationId = command.SurveyId
            };

        }
    }
}
