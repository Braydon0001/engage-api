namespace Engage.Application.Services.EmployeeCoolerBoxes.Commands;

public class EmployeeCoolerBoxCreateCommand : EmployeeCoolerBoxCommand, IMapTo<EmployeeCoolerBox>, IRequest<OperationStatus>
{
    public bool SaveChanges { get; set; } = true;

    public void Mapping(Profile profile)
    {

        profile.CreateMap<EmployeeCoolerBoxCommand, EmployeeCoolerBox>();
    }
}

public class EmployeeCoolerBoxCreateHandler : BaseCreateCommandHandler, IRequestHandler<EmployeeCoolerBoxCreateCommand, OperationStatus>
{
    public EmployeeCoolerBoxCreateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeCoolerBoxCreateCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<EmployeeCoolerBoxCreateCommand, EmployeeCoolerBox>(command);
        _context.EmployeeCoolerBoxes.Add(entity);

        if (command.SaveChanges)
        {
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.EmployeeCoolerBoxId;
            return opStatus;
        }

        return new OperationStatus(true);
    }
}

public class EmployeeCoolerBoxCreateValidator : EmployeeCoolerBoxValidator<EmployeeCoolerBoxCreateCommand>
{
    public EmployeeCoolerBoxCreateValidator()
    {
    }
}