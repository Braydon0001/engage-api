namespace Engage.Application.Services.EngageRegionClaimManagers.Commands;

public record EngageRegionClaimManagerDeleteCommand(int EngageRegionId, int UserId) : IRequest<EngageRegionClaimManager>
{
}

public class EngageRegionClaimManagerDeleteHandler : IRequestHandler<EngageRegionClaimManagerDeleteCommand, EngageRegionClaimManager>
{
    private readonly IAppDbContext _context;

    public EngageRegionClaimManagerDeleteHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<EngageRegionClaimManager> Handle(EngageRegionClaimManagerDeleteCommand query, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageRegionClaimManagers.SingleOrDefaultAsync(e => e.EngageRegionId == query.EngageRegionId && e.UserId == query.UserId, cancellationToken);
        if (entity == null)
        {
            return null;
        }

        _context.EngageRegionClaimManagers.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}

public class EngageRegionClaimManagerValidator : AbstractValidator<EngageRegionClaimManagerDeleteCommand>
{
    public EngageRegionClaimManagerValidator()
    {
        RuleFor(e => e.EngageRegionId).GreaterThan(0);
        RuleFor(e => e.UserId).GreaterThan(0);
    }
}