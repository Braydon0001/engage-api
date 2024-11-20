namespace Engage.Application.Services.EmployeeVehicles.Commands;

public class EmployeeVehicleUpdateCommand : EmployeeVehicleCommand, IMapTo<EmployeeVehicle>, IRequest<OperationStatus>
{
    public int Id { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeVehicleCommand, EmployeeVehicle>();
    }
}

public class UpdateVehicleCommandHandler : BaseUpdateCommandHandler, IRequestHandler<EmployeeVehicleUpdateCommand, OperationStatus>
{
    public UpdateVehicleCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeVehicleUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeVehicles.SingleAsync(x => x.EmployeeVehicleId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeVehicleId;
        return opStatus;
    }
}

public class UpdateEmployeeVehicleValidator : EmployeeVehicleValidator<EmployeeVehicleUpdateCommand>
{
    public UpdateEmployeeVehicleValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}