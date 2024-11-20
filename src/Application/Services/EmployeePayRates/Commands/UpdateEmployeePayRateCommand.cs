namespace Engage.Application.Services.EmployeePayRates.Commands
{
    public class UpdateEmployeePayRateCommand : EmployeePayRateCommand, IRequest<OperationStatus>
    {
        public int Id { get; set; }
    }

    public class UpdateEmployeePayRateCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmployeePayRateCommand, OperationStatus>
    {
        public UpdateEmployeePayRateCommandHandler(IAppDbContext context, IMapper mapper, IMediator mediator) : 
            base(context, mapper, mediator) { }

        public async Task<OperationStatus> Handle(UpdateEmployeePayRateCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.EmployeePayRates
                .FirstOrDefaultAsync(x => x.EmployeePayRateId == command.Id);

            if (entity == null)
                throw new NotFoundException(nameof(EmployeePayRate), command.Id);

            _mapper.Map(command, entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = command.Id;
            return opStatus;
        }
    }
}
