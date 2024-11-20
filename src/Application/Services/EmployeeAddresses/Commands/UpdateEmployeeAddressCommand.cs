namespace Engage.Application.Services.EmployeeAddresses.Commands
{
    public class UpdateEmployeeAddressCommand : EmployeeAddressCommand, IRequest<OperationStatus>
    {
        public int Id { get; set; }
    }

    public class UpdateEmployeeAddressCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeeAddressCommand, OperationStatus>
    {
        public UpdateEmployeeAddressCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : 
            base(context, mapper, mediator) { }

        public async Task<OperationStatus> Handle(UpdateEmployeeAddressCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.EmployeeAddresses
                .FirstOrDefaultAsync(x => x.EmployeeAddressId == command.Id);

            if (entity == null)
                throw new NotFoundException(nameof(EmployeeAddress), command.Id);

            _mapper.Map(command, entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = command.Id;
            return opStatus;
        }
    }
}
