using Engage.Application.Services.EngageCategories.Models;

namespace Engage.Application.Services.EngageCategories.Queries;

public class EngageCategoryVmQuery : IRequest<EngageCategoryVm>
{
    public int Id { get; set; }
}

public class EngageCategoryVmQueryHandler : BaseQueryHandler, IRequestHandler<EngageCategoryVmQuery, EngageCategoryVm>
{
    public EngageCategoryVmQueryHandler(IAppDbContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<EngageCategoryVm> Handle(EngageCategoryVmQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.EngageCategories.Include(e => e.EngageSubGroup)
                                                    .SingleAsync(e => e.Id == request.Id, cancellationToken);

        return _mapper.Map<EngageCategory, EngageCategoryVm>(entity);
    }
}
