namespace Engage.Application.Services.Stakeholders.Commands.CreateStakeholder;

public class CreateStakeholderCommand : IRequest<OperationStatus>
{
    public StakeholderTypes StakeholderType { get; set; }
}

public class CreateStakeholderCommandHandler : IRequestHandler<CreateStakeholderCommand, OperationStatus>
{
    private readonly IAppDbContext _context;
    public CreateStakeholderCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(CreateStakeholderCommand request, CancellationToken cancellationToken)
    {
        var entity = new Stakeholder { StakeholderType = request.StakeholderType };
        _context.Stakeholders.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.ReturnObject = entity;
        return opStatus;
    }
}
