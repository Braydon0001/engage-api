namespace Engage.Application.Services.EmployeeAddresses.Commands
{
    public class CreateEmployeeAddressCommand : EmployeeAddressCommand, IRequest<OperationStatus>
    { }

    public class CreateEmployeeAddressCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEmployeeAddressCommand, OperationStatus>
    {
        public CreateEmployeeAddressCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) :
            base(context, mapper, mediator)
        { }

        public async Task<OperationStatus> Handle(CreateEmployeeAddressCommand command, CancellationToken cancellationToken)
        {
            var existingAddress = await _context.EmployeeAddresses
                                                    .Where(c => c.EmployeeId == command.EmployeeId
                                                        && c.Deleted == false)
                                                    .FirstOrDefaultAsync();

            if (existingAddress != null)
            {
                throw new UniqueException("This Employee Already has an Address. \n\n It can't be added again.");
            }

            var entity = _mapper.Map<CreateEmployeeAddressCommand, EmployeeAddress>(command);
            _context.EmployeeAddresses.Add(entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);

            return opStatus;
        }
    }
}
