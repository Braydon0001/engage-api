namespace Engage.Application.Services.EmploymentTypes.Commands
{
    public class UpdateEmploymentTypeCommand : EmploymentTypeCommand, IRequest<OperationStatus>
    {
        public int Id { get; set; }
    }

    public class UpdateEmploymentTypeCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateEmploymentTypeCommand, OperationStatus>
    {
        public UpdateEmploymentTypeCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<OperationStatus> Handle(UpdateEmploymentTypeCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.EmploymentTypes.SingleAsync(x => x.Id == command.Id, cancellationToken);
            _mapper.Map(command, entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.Id;
            return opStatus;
        }
    }
}
