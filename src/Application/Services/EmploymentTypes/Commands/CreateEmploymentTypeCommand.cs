namespace Engage.Application.Services.EmploymentTypes.Commands
{
    public class CreateEmploymentTypeCommand : EmploymentTypeCommand, IRequest<OperationStatus>
    {
    }

    public class CreateEmploymentTypeCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateEmploymentTypeCommand, OperationStatus>
    {
        public CreateEmploymentTypeCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<OperationStatus> Handle(CreateEmploymentTypeCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CreateEmploymentTypeCommand, EmploymentType>(command);
            _context.EmploymentTypes.Add(entity);

            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.Id;
            return opStatus;
        }
    }
}
