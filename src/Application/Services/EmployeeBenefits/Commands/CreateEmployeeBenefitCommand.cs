namespace Engage.Application.Services.EmployeeBenefits.Commands
{
    public class CreateEmployeeBenefitCommand : EmployeeBenefitCommand, IRequest<OperationStatus>
    {
        public int EmployeeId { get; set; }
    }

    public class CreateEmployeeBenefitCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEmployeeBenefitCommand, OperationStatus>
    {
        
        public CreateEmployeeBenefitCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<OperationStatus> Handle(CreateEmployeeBenefitCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CreateEmployeeBenefitCommand, EmployeeBenefit>(command);
            entity.EmployeeId = command.EmployeeId;
            _context.EmployeeBenefits.Add(entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.EmployeeBenefitId;
            return opStatus;
        }
    }
}
