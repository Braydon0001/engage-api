using Engage.Application.Services.EngageCategories.Models;

namespace Engage.Application.Services.EngageCategories.Queries;

public class EngageBrandVmQuery : IRequest<EngageBrandVm>
{
    public int Id { get; set; }
}

public class EngageBrandVmQueryHandler : BaseQueryHandler, IRequestHandler<EngageBrandVmQuery, EngageBrandVm>
{
    public EngageBrandVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EngageBrandVm> Handle(EngageBrandVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageBrands.Include(e => e.Id)
                                                    .SingleAsync(e => e.Id == request.Id, cancellationToken);

        return _mapper.Map<EngageBrand, EngageBrandVm>(entity);
    }
}
