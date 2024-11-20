namespace Engage.Application.Services.EmployeeVehicles.Commands;

public class EmployeeVehicleCreateCommand : EmployeeVehicleCommand, IMapTo<EmployeeVehicle>, IRequest<OperationStatus>
{
    public bool SaveChanges { get; set; } = true;
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeVehicleCommand, EmployeeVehicle>();
    }
}

public class EmployeeVehicleCreateHandler : BaseCreateCommandHandler, IRequestHandler<EmployeeVehicleCreateCommand, OperationStatus>
{
    public EmployeeVehicleCreateHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeVehicleCreateCommand command, CancellationToken cancellationToken)
    {
        var existingRegistrationClaimNumber = await _context.EmployeeVehicles
                                                .Where(c => c.RegistrationNumber == command.RegistrationNumber
                                                    && c.Deleted == false)
                                                .FirstOrDefaultAsync();

        if (existingRegistrationClaimNumber != null)
        {
            throw new UniqueException("This Registration Number Already Exists. \n\n It can't be added again.");
        }


        var entity = _mapper.Map<EmployeeVehicleCreateCommand, EmployeeVehicle>(command);
        _context.EmployeeVehicles.Add(entity);

        if (command.SaveChanges)
        {
            var opStatus = await _context.SaveChangesAsync(cancellationToken);
            opStatus.OperationId = entity.EmployeeVehicleId;
            return opStatus;
        }

        return new OperationStatus(true);

    }
}

public class EmployeeVehicleCreateValidator : EmployeeVehicleValidator<EmployeeVehicleCreateCommand>
{
    public EmployeeVehicleCreateValidator()
    {
    }
}