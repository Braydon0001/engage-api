namespace Engage.Application.Services.DCDepartments.Commands
{
    public class CreateDCDepartmentCommand : DCDepartmentCommand, IRequest<OperationStatus>
    {
    }

    public class CreateDCDepartmentCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateDCDepartmentCommand, OperationStatus>
    {
        public CreateDCDepartmentCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<OperationStatus> Handle(CreateDCDepartmentCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CreateDCDepartmentCommand, DCDepartment>(command);
            _context.DCDepartments.Add(entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.DCDepartmentId;
            return opStatus;
        }
    }
}
