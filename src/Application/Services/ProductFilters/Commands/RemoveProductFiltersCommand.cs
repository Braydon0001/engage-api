namespace Engage.Application.Services.ProductFilters.Commands;

public class RemoveProductFiltersCommand : IRequest<OperationStatus>
{
    public string Filter { get; set; }
}

public class RemoveProductFiltersCommandHandler : IRequestHandler<RemoveProductFiltersCommand, OperationStatus>
{
    private readonly IAppDbContext _context;

    public RemoveProductFiltersCommandHandler(IAppDbContext context)
    {
        _context = context;
    }

    public async Task<OperationStatus> Handle(RemoveProductFiltersCommand request, CancellationToken cancellationToken)
    {
        var entities = await _context.ProductFilters.Where(e => e.Filter == request.Filter)
                                                .ToListAsync(cancellationToken);

        _context.ProductFilters.RemoveRange(entities);

        return await _context.SaveChangesAsync(cancellationToken);
    }
}