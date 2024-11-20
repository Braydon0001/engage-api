namespace Engage.Application.Services.ClaimYears.Commands;

public class CreateClaimYearCommand : ClaimYearCommand, IRequest<OperationStatus>
{

}

public class CreateClaimYearCommandHandler : BaseCreateCommandHandler, IRequestHandler<CreateClaimYearCommand, OperationStatus>
{
    public CreateClaimYearCommandHandler(IAppDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<OperationStatus> Handle(CreateClaimYearCommand command, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<CreateClaimYearCommand, ClaimYear>(command);
        _context.ClaimYears.Add(entity);

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        opStatus.OperationId = entity.ClaimYearId;
        return opStatus;
    }
}
