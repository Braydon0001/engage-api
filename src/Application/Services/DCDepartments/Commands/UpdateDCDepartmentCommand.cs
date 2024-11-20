namespace Engage.Application.Services.DCDepartments.Commands
{
    public class UpdateDCDepartmentCommand : DCDepartmentCommand, IRequest<OperationStatus>
    {
        public int Id { get; set; }
    }

    public class UpdateDCDepartmentCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateDCDepartmentCommand, OperationStatus>
    {
        public UpdateDCDepartmentCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<OperationStatus> Handle(UpdateDCDepartmentCommand command, CancellationToken cancellationToken)
        {
            var entity = await _context.DCDepartments.SingleAsync(x => x.DCDepartmentId == command.Id, cancellationToken);
            _mapper.Map(command, entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.DCDepartmentId;
            return opStatus;
        }
    }
}
