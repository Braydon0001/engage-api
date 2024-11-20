namespace Engage.Application.Services.EmployeePayRates.Commands
{
    public class CreateEmployeePayRateCommand : EmployeePayRateCommand, IRequest<OperationStatus>
    { }

    public class CreateEmployeePayRateCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEmployeePayRateCommand, OperationStatus>
    {
        public CreateEmployeePayRateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) :
            base(context, mapper, mediator)
        { }

        public async Task<OperationStatus> Handle(CreateEmployeePayRateCommand command, CancellationToken cancellationToken)
        {
            var existingAddress = await _context.EmployeePayRates
                                                    .Where(c => c.EmployeeId == command.EmployeeId
                                                        && c.Deleted == false)
                                                    .FirstOrDefaultAsync();

            if (existingAddress != null)
            {
                throw new UniqueException("This Employee Already has an Address. \n\n It can't be added again.");
            }

            var entity = _mapper.Map<CreateEmployeePayRateCommand, EmployeePayRate>(command);
            _context.EmployeePayRates.Add(entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);

            return opStatus;
        }
    }
}
