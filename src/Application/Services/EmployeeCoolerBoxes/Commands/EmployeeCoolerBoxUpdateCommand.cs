namespace Engage.Application.Services.EmployeeCoolerBoxes.Commands;

public class EmployeeCoolerBoxUpdateCommand : EmployeeCoolerBoxCommand, IMapTo<EmployeeCoolerBox>, IRequest<OperationStatus>
{
    public int Id { get; set; }
    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeCoolerBoxCommand, EmployeeCoolerBox>();
    }
}

public class UpdateCoolerBoxCommandHandler : BaseUpdateCommandHandler, IRequestHandler<EmployeeCoolerBoxUpdateCommand, OperationStatus>
{
    public UpdateCoolerBoxCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OperationStatus> Handle(EmployeeCoolerBoxUpdateCommand command, CancellationToken cancellationToken)
    {
        var entity = await _context.EmployeeCoolerBoxes.SingleAsync(x => x.EmployeeCoolerBoxId == command.Id, cancellationToken);
        _mapper.Map(command, entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.EmployeeCoolerBoxId;
        return opStatus;
    }
}

public class UpdateEmployeeCoolerBoxValidator : EmployeeCoolerBoxValidator<EmployeeCoolerBoxUpdateCommand>
{
    public UpdateEmployeeCoolerBoxValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).NotEmpty();
    }
}