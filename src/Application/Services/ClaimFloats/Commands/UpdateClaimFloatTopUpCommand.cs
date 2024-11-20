namespace Engage.Application.Services.ClaimFloats.Commands;

public class UpdateClaimFloatTopUpCommand : IRequest<OperationStatus>
{
    public List<int> ClaimFloatIds { get; set; }
    public decimal? TopUpAmount { get; set; }
}

public class UpdateClaimFloatTopUpCommandHandler : BaseUpdateCommandHandler, IRequestHandler<UpdateClaimFloatTopUpCommand, OperationStatus>
{
    private readonly IUserService _user;
    public UpdateClaimFloatTopUpCommandHandler(IAppDbContext context, IMapper mapper, IUserService user) : base(context, mapper)
    {
        _user = user;
    }

    public async Task<OperationStatus> Handle(UpdateClaimFloatTopUpCommand request, CancellationToken cancellationToken)
    {
        foreach (var id in request.ClaimFloatIds)
        {
            var entity = await _context.ClaimFloats.SingleOrDefaultAsync(x => x.ClaimFloatId == id);
            entity.TopUpAmount = request.TopUpAmount.Value;
            entity.LastToppedUp = DateTime.Now;
            entity.LastToppedUpBy = _user.UserName;
            entity.Amount = entity.Amount + request.TopUpAmount.Value;

            var claimFloatTopUpHistory = new ClaimFloatTopUpHistory
            {
                TopUpAmount = request.TopUpAmount.Value,
                ClaimFloatId = id,
            };

            _context.ClaimFloatTopUpHistories.Add(claimFloatTopUpHistory);
        }

        var opStatus = await _context.SaveChangesAsync(cancellationToken);
        return opStatus;
    }
}
